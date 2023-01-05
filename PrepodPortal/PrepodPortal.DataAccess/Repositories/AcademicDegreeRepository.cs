using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class AcademicDegreeRepository : IAcademicDegreeRepository
{
    private readonly PrepodPortalDbContext _context;

    public AcademicDegreeRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(AcademicDegree academicDegree)
    {
        await _context.AcademicDegrees.AddAsync(academicDegree);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<AcademicDegree?> GetAsync(long id) =>
        await _context.AcademicDegrees
            .Include(degree => degree.User)
            .FirstOrDefaultAsync(degree => degree.Id == id);

    public async Task<bool> RemoveAsync(AcademicDegree academicDegree)
    {
        _context.AcademicDegrees.Remove(academicDegree);
        return await _context.SaveChangesAsync() > 0;
    }
}