using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly PrepodPortalDbContext _context;

    public EducationRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(Education education)
    {
        await _context.Educations.AddAsync(education);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Education?> GetAsync(long id) =>
        await _context.Educations
            .Include(education => education.User)
            .FirstOrDefaultAsync(education => education.Id == id);

    public async Task<bool> RemoveAsync(Education education)
    {
        _context.Educations.Remove(education);
        return await _context.SaveChangesAsync() > 0;
    }
}