using Microsoft.AspNetCore.Http;

namespace PrepodPortal.Common.DTO;

public class ChangeUserAvatarDto
{
    public IFormFile AvatarImageFile { get; set; }

    public string UserId { get; set; }
}