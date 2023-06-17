using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces
{
    public interface IQualificationIncreaseRepository
    {
        Task<ICollection<QualificationIncrease>> GetAllAsync(string userId);
    }
}
