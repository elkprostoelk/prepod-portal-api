using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IPublicationService
{
    Task<bool> ExistsAsync(long id);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ICollection<PublicationDto>> GetAllAsync(string userId);
}