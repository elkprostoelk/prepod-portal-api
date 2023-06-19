using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces
{
    public interface IQualificationIncreaseRepository
    {
        Task<bool> DeleteAsync(QualificationIncrease qualIncrease);
        Task<bool> ExistsAsync(long id);
        Task<ICollection<QualificationIncrease>> GetAllAsync(string userId);
        Task<QualificationIncrease?> GetAsync(long id);
    }
}
