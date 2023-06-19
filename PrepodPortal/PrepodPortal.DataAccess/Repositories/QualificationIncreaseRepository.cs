using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories
{
    public class QualificationIncreaseRepository : IQualificationIncreaseRepository
    {
        private readonly PrepodPortalDbContext _context;

        public QualificationIncreaseRepository(PrepodPortalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(QualificationIncrease qualIncrease)
        {
            _context.QualificationIncreases.Remove(qualIncrease);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(long id) =>
            await _context.QualificationIncreases.AnyAsync(x => x.Id == id);

        public async Task<ICollection<QualificationIncrease>> GetAllAsync(string userId) =>
            await _context.QualificationIncreases
                .AsNoTracking()
                .Where(qi => qi.UserId == userId)
                .ToListAsync();

        public async Task<QualificationIncrease?> GetAsync(long id) =>
            await _context.QualificationIncreases
                .AsNoTracking()
                .SingleOrDefaultAsync(qi => qi.Id == id);
    }
}
