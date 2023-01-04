using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IScientometricDbProfileService
{
    Task<bool> AddProfilesAsync(ICollection<NewScientometricDbProfileDto> scientometricDbProfiles, string userId);
}