using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string idOrEmail, CancellationToken cancellationToken = default);
    Task<UserTokenDto?> ValidateUserAsync(LoginDto loginDto);
    Task<bool> RegisterTeacherAsync(NewTeacherDto newTeacherDto);
    Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> DeleteUserAsync(string id);
}