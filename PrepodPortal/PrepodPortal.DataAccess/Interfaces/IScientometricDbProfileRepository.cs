using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IScientometricDbProfileRepository
{
    Task<bool> AddRangeAsync(ICollection<ScientometricDbProfile> profiles);
}