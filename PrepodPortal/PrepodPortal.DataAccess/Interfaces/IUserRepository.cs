using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string idOrEmail, CancellationToken cancellationToken);
    Task<ApplicationUser?> GetAsync(string email);
}