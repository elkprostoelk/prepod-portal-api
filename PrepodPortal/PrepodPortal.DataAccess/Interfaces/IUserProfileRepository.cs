using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IUserProfileRepository
{
    Task<bool> AddAsync(UserProfile userProfile);
}