using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string email);
    Task<ApplicationUser?> GetAsync(string email);
}