using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly PrepodPortalDbContext _context;

    public UserProfileRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(UserProfile userProfile)
    {
        await _context.UserProfiles.AddAsync(userProfile);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ICollection<UserProfile>> GetAllAsync(string? searchByName)
    {
        IQueryable<UserProfile> profiles = _context.UserProfiles
            .Include(profile => profile.Department)
            .Where(profile => !profile.Name.Contains("Admin"));
        if (!String.IsNullOrEmpty(searchByName))
        {
            profiles = profiles.Where(profile =>
                profile.Name.Trim().ToLower().Contains(
                    searchByName.Trim().ToLower()));
        }

        return await profiles.ToListAsync();
    }
}