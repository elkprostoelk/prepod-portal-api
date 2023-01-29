using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class SchoolBookRepository : ISchoolBookRepository
{
    private readonly PrepodPortalDbContext _context;

    public SchoolBookRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(SchoolBook schoolBook)
    {
        await _context.SchoolBooks.AddAsync(schoolBook);
        return await _context.SaveChangesAsync() > 0;
    }
}