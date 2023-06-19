using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces
{
    public interface IQualificationIncreaseService
    {
        Task<ServiceResult> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
        Task<ICollection<QualificationIncreaseDto>> GetAllAsync(string userId);
    }
}
