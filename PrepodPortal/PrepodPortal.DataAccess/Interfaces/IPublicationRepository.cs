using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IPublicationRepository
{
    Task<bool> ExistsAsync(long id);
    Task<ICollection<Publication>> GetAllAsync(string userId);
    Task<Publication?> GetAsync(long id);
    Task<bool> RemoveAsync(Publication publication);
}