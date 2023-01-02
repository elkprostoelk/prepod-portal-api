using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrepodPortal.Common.Configurations;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class UserService : IUserService
{
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IScientometricDbProfileService _scientometricDbProfileService;
    private readonly IEmailService _emailService;

    public UserService(
        IPasswordGenerator passwordGenerator,
        ILogger<UserService> logger,
        IUserRepository repository,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtConfiguration> jwtConfiguration,
        IUserProfileRepository userProfileRepository,
        IScientometricDbProfileService scientometricDbProfileService,
        IEmailService emailService
        )
    {
        _passwordGenerator = passwordGenerator;
        _logger = logger;
        _repository = repository;
        _userManager = userManager;
        _userProfileRepository = userProfileRepository;
        _scientometricDbProfileService = scientometricDbProfileService;
        _emailService = emailService;
        _jwtConfiguration = jwtConfiguration.Value;
    }
    
    public async Task<bool> UserExistsAsync(string idOrEmail, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.ExistsAsync(idOrEmail, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    public async Task<UserTokenDto?> ValidateUserAsync(LoginDto loginDto)
    {
        try
        {
            var user = await _repository.GetAsync(loginDto.Email);
            if (user is null)
            {
                return null;
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            return passwordValid ? new UserTokenDto {Jwt = await GenerateTokenAsync(user)} : null;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return null;
        }
    }

    public async Task<bool> RegisterTeacherAsync(NewTeacherDto newTeacherDto)
    {
        try
        {
            var password = _passwordGenerator.GeneratePassword(8, 2);
            var newUser = new ApplicationUser
            {
                UserName = newTeacherDto.Email,
                Email = newTeacherDto.Email
            };
            var creationResult = await _userManager.CreateAsync(newUser, password);
            var succeeded = creationResult.Succeeded;
            if (succeeded)
            {
                var roleCreationResult = await _userManager.AddToRoleAsync(newUser, "user");
                succeeded = succeeded && roleCreationResult.Succeeded;
                var userProfile = new UserProfile
                {
                    UserId = newUser.Id,
                    Name = newTeacherDto.Name,
                    DepartmentId = newTeacherDto.DepartmentId,
                    ScientometricDbProfiles = new List<ScientometricDbProfile>(),
                    AvatarImagePath = "/Images/no-avatar.png"
                };
                var userProfileAdded = await _userProfileRepository.AddAsync(userProfile);
                succeeded = succeeded && userProfileAdded;
                var scientometricDbProfilesAdded = await _scientometricDbProfileService
                    .AddProfilesAsync(
                        newTeacherDto.ScientometricDbProfiles,
                        userProfile.Id
                    );
                succeeded = succeeded && scientometricDbProfilesAdded;
                Directory.CreateDirectory($"{Environment.CurrentDirectory}/Users/{newTeacherDto.Email}/avatar");
                await _emailService.SendEmailAsync(newTeacherDto.Name, newTeacherDto.Email, password);
            }
            return succeeded;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
            if (user is null)
            {
                return false;
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDto.CurrentPassword,
                changePasswordDto.NewPassword);
            return changePasswordResult.Succeeded;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    private async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfiguration.Secret);
        var secret = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtConfiguration.ExpiresIn),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}