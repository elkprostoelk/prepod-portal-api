namespace PrepodPortal.Core.Interfaces;

public interface IDepartmentService
{
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
}