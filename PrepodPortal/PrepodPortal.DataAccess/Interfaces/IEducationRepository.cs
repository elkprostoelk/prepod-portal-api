using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IEducationRepository
{
    Task<bool> AddAsync(Education education);
    Task<Education?> GetAsync(long id);
    Task<bool> RemoveAsync(Education education);
}