using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PrepodPortalDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(
        PrepodPortalDbContext context,
        UserManager<ApplicationUser> userManager
        )
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> ExistsAsync(string idOrEmail, CancellationToken cancellationToken) =>
        await _userManager.Users
            .AnyAsync(user => user.Email == idOrEmail
                        || user.Id == idOrEmail, cancellationToken);

    public async Task<ApplicationUser?> GetAsync(string email) =>
        await _userManager.FindByEmailAsync(email);
}