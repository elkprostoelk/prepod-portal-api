using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PrepodPortal.Common.Configurations;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PrepodPortalDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AdminUserConfiguration _adminUserConfig;

    public UserRepository(
        PrepodPortalDbContext context,
        UserManager<ApplicationUser> userManager,
        IOptions<AdminUserConfiguration> adminUserConfigOptions)
    {
        _context = context;
        _userManager = userManager;
        _adminUserConfig = adminUserConfigOptions.Value;
    }

    public async Task<bool> ExistsAsync(string idOrEmail, CancellationToken cancellationToken) =>
        await _userManager.Users
            .AnyAsync(user => user.Email == idOrEmail
                        || user.Id == idOrEmail, cancellationToken);

    public async Task<ApplicationUser?> GetAsync(string email) =>
        await _userManager.FindByEmailAsync(email);

    public async Task<bool> UpdateAsync(ApplicationUser user)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ICollection<ApplicationUser>> GetAllAsync(string? userId)
    {
        var users = _context.Users
            .AsNoTracking()
            .Include(user => user.Department)
            .Where(user =>
                user.Email != _adminUserConfig.Email);

        if (!String.IsNullOrEmpty(userId))
        {
            users = users.Where(user => user.Id != userId);
        }
        
        return await users.ToListAsync();
    }

    public async Task<ApplicationUser?> GetFullAsync(string userId) =>
        await _userManager.Users
            .AsNoTracking()
            .Include(user => user.Department)
            .Include(user => user.AcademicDegrees)
            .Include(user => user.ScientometricDbProfiles)
            .Include(user => user.Educations)
            .Include(user => user.DissertationDefenses)
            .FirstOrDefaultAsync(user => user.Id == userId);
}