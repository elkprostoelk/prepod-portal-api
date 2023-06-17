using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces
{
    public interface IQualificationIncreaseService
    {
        Task<ICollection<QualificationIncreaseDto>> GetAllAsync(string userId);
    }
}
