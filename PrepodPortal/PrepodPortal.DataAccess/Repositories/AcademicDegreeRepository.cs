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
}