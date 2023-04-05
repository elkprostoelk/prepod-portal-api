using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IScientometricDbProfileService
{
    Task<ServiceResult<ICollection<long>>> AddProfilesAsync(ICollection<ScientometricDbProfileDto> scientometricDbProfiles, string userId);
}