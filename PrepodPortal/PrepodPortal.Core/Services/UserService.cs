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
    private readonly IScientometricDbProfileService _scientometricDbProfileService;
    private readonly IEmailService _emailService;

    public UserService(
        IPasswordGenerator passwordGenerator,
        ILogger<UserService> logger,
        IUserRepository repository,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtConfiguration> jwtConfiguration,
        IScientometricDbProfileService scientometricDbProfileService,
        IEmailService emailService
        )
    {
        _passwordGenerator = passwordGenerator;
        _logger = logger;
        _repository = repository;
        _userManager = userManager;
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

    public async Task<ServiceResult<UserTokenDto>> ValidateUserAsync(LoginDto loginDto)
    {
        var result = new ServiceResult<UserTokenDto>();
        try
        {
            var user = await _repository.GetAsync(loginDto.Email);
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add($"User {loginDto.Email} was not found!");
            }
            else
            {
                var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordValid)
                {
                    result.Container = new UserTokenDto
                    {
                        Jwt = await GenerateTokenAsync(user)
                    };
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Errors.Add("Invalid password!");
                }
            }
            
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add("Failed to validate the user!");
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }

    public async Task<ServiceResult<string>> RegisterTeacherAsync(NewTeacherDto newTeacherDto)
    {
        var result = new ServiceResult<string>();
        try
        {
            var password = _passwordGenerator.GeneratePassword(8, 2);
            var newUser = new ApplicationUser
            {
                UserName = newTeacherDto.Email,
                Email = newTeacherDto.Email,
                Name = newTeacherDto.Name,
                DepartmentId = newTeacherDto.DepartmentId,
                ScientometricDbProfiles = new List<ScientometricDbProfile>(),
                AvatarImagePath = "/Images/no-avatar.png"
            };
            var creationResult = await _userManager.CreateAsync(newUser, password);
            result.IsSuccessful = creationResult.Succeeded;
            if (result.IsSuccessful)
            {
                var roleCreationResult = await _userManager.AddToRoleAsync(newUser, "user");
                result.IsSuccessful = result.IsSuccessful && roleCreationResult.Succeeded;
                if (result.IsSuccessful)
                {
                    var scientometricDbProfilesAddingResult = await _scientometricDbProfileService
                        .AddProfilesAsync(
                            newTeacherDto.ScientometricDbProfiles,
                            newUser.Id
                        );
                    result.IsSuccessful = result.IsSuccessful && scientometricDbProfilesAddingResult.IsSuccessful;
                    if (result.IsSuccessful)
                    {
                        Directory.CreateDirectory($"{Environment.CurrentDirectory}/Users/{newTeacherDto.Email}/avatar");
                        await _emailService.SendEmailAsync(newTeacherDto.Name, newTeacherDto.Email, password);
                    }
                    else
                    {
                        result.Errors.Add("Failed to add scientometric DB profiles");
                    }
                }
                else
                {
                    result.Errors.Add("Failed to add the user to a role!");
                }
            }
            else
            {
                result.Errors.Add("Failed to create a user!");
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add("Failed to register a teacher!");
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }

    public async Task<ServiceResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var result = new ServiceResult();
        const string commonError = "Failed to change the password!";
        try
        {
            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("User does not exist!");
            }
            else
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(
                    user,
                    changePasswordDto.CurrentPassword,
                    changePasswordDto.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add(commonError);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add(commonError);
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }

    public async Task<ServiceResult> DeleteUserAsync(string id)
    {
        var result = new ServiceResult();
        const string commonError = "Failed to delete a user!";
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("User does not exist!");
            }
            else
            {
                var deletingResult = await _userManager.DeleteAsync(user);
                if (!deletingResult.Succeeded)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add(commonError);
                }
            }
            
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add(commonError);
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }

    public async Task<ServiceResult> ChangeAvatarAsync(ChangeUserAvatarDto changeUserAvatarDto)
    {
        var result = new ServiceResult();
        const string commonError = "Failed to change user's avatar";
        try
        {
            var user = await _userManager.FindByIdAsync(changeUserAvatarDto.UserId);
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("User does not exist!");
            }
            else
            {
                var userAvatarFolderPath = $"Users/{user.Email}/avatar";
                var fullUserAvatarFolderPath = $"{Environment.CurrentDirectory}/{userAvatarFolderPath}";
                var pathExists = Directory.Exists(userAvatarFolderPath);
                if (!pathExists)
                {
                    Directory.CreateDirectory(fullUserAvatarFolderPath);
                }

                var directoryInfo = new DirectoryInfo(fullUserAvatarFolderPath).GetFiles();
                if (directoryInfo.Any())
                {
                    foreach (var file in directoryInfo)
                    {
                        file.Delete();
                    }
                }

                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(changeUserAvatarDto.AvatarImageFile.FileName)}";
                var filePath = $"{userAvatarFolderPath}/{newFileName}";
                var fullFilePath = $"{fullUserAvatarFolderPath}/{newFileName}";
                await using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await changeUserAvatarDto.AvatarImageFile.CopyToAsync(stream);
                }

                user.AvatarImagePath = filePath;
                var updated = await _repository.UpdateAsync(user);
                if (!updated)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add(commonError);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add(commonError);
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
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