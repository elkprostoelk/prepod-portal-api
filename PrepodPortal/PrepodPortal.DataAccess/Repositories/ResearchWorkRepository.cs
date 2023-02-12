using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class ResearchWorkRepository : IResearchWorkRepository
{
    private readonly PrepodPortalDbContext _context;

    public ResearchWorkRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(ResearchWork researchWork)
    {
        await _context.ResearchWorks.AddAsync(researchWork);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ResearchWork?> GetAsync(long id) =>
        await _context.ResearchWorks.FirstOrDefaultAsync(researchWork =>
            researchWork.Id == id);

    public async Task<bool> RemoveAsync(ResearchWork researchWork)
    {
        _context.ResearchWorks.Remove(researchWork);
        return await _context.SaveChangesAsync() > 0;
    }
}