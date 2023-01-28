using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class PublicationRepository : IPublicationRepository
{
    private readonly PrepodPortalDbContext _context;

    public PublicationRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(long id) =>
        await _context.Publications.AnyAsync(publication => publication.Id == id);

    public async Task<Publication?> GetAsync(long id) =>
        await _context.Publications.FirstOrDefaultAsync(publication => publication.Id == id);

    public async Task<bool> RemoveAsync(Publication publication)
    {
        _context.Publications.Remove(publication);
        return await _context.SaveChangesAsync() > 0;
    }
}