using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IUserProfileService
{
    Task<ICollection<BriefUserProfileDto>> GetAllAsync(string? searchByName);
}