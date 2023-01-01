using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class ScientometricDbProfileRepository : IScientometricDbProfileRepository
{
    private readonly PrepodPortalDbContext _context;

    public ScientometricDbProfileRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddRangeAsync(ICollection<ScientometricDbProfile> profiles)
    {
        await _context.ScientometricDbProfiles.AddRangeAsync(profiles);
        return await _context.SaveChangesAsync() > 0;
    }
}