using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class LectureThesesRepository : ILectureThesesRepository
{
    private readonly PrepodPortalDbContext _context;

    public LectureThesesRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(LectureTheses lectureTheses)
    {
        await _context.LectureTheses.AddAsync(lectureTheses);
        return await _context.SaveChangesAsync() > 0;
    }
}