using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class MonographRepository : IMonographRepository
{
    private readonly PrepodPortalDbContext _context;

    public MonographRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(Monograph monograph)
    {
        await _context.Monographs.AddAsync(monograph);
        return await _context.SaveChangesAsync() > 0;
    }
}