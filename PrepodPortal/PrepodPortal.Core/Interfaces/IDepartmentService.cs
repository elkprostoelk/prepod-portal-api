using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IDepartmentService
{
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    Task<ICollection<DepartmentDto>> GetAllAsync();
}