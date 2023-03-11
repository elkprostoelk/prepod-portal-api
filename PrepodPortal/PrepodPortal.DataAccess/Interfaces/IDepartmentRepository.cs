using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IDepartmentRepository
{
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
    Task<ICollection<Department>> GetAllAsync();
}