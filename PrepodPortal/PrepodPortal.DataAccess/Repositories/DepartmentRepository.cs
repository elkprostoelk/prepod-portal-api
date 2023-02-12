using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly PrepodPortalDbContext _context;

    public DepartmentRepository(PrepodPortalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default) =>
        await _context.Departments.AnyAsync(
            department => department.Id == id,
            cancellationToken);
}