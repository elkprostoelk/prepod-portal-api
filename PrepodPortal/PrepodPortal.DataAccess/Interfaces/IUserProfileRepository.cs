using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IUserProfileRepository
{
    Task<bool> AddAsync(UserProfile userProfile);
    Task<ICollection<UserProfile>> GetAllAsync(string? searchByName);
}