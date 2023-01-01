using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string email);
    Task<UserTokenDto?> ValidateUserAsync(LoginDto loginDto);
    Task<bool> RegisterTeacherAsync(NewTeacherDto newTeacherDto);
}