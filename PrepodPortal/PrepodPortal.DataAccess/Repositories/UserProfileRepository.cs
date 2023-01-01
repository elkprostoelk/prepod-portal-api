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
}