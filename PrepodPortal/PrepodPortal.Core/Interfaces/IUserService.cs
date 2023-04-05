using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string idOrEmail, CancellationToken cancellationToken = default);
    Task<ServiceResult<UserTokenDto>> ValidateUserAsync(LoginDto loginDto);
    Task<ServiceResult<string>> RegisterTeacherAsync(NewTeacherDto newTeacherDto);
    Task<ServiceResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<ServiceResult> DeleteUserAsync(string id);
    Task<ServiceResult> ChangeAvatarAsync(ChangeUserAvatarDto changeUserAvatarDto);
    Task<ServiceResult> DeleteAvatarAsync(string id);
    Task<ServiceResult<ICollection<BriefUserProfileDto>>> GetAllTeachersAsync(string? userId);
    Task<UserMainInfoDto?> GetMainInfoAsync(string userId);
    Task<UserNameAndAvatarDto?> GetAvatarAndNameAsync(string userId);
}