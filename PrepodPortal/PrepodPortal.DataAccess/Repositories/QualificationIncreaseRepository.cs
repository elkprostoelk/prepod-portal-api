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

        public async Task<ICollection<QualificationIncrease>> GetAllAsync(string userId) =>
            await _context.QualificationIncreases
                .AsNoTracking()
                .Where(qi => qi.UserId == userId)
                .ToListAsync();
    }
}
