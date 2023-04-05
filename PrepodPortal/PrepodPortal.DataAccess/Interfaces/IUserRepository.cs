using PrepodPortal.Common.DTO;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string idOrEmail, CancellationToken cancellationToken);
    Task<ApplicationUser?> GetAsync(string email);
    Task<bool> UpdateAsync(ApplicationUser user);
    Task<ICollection<ApplicationUser>> GetAllAsync(string? userId);
    Task<ApplicationUser?> GetFullAsync(string userId);
}