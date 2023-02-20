using FluentValidation;
using Microsoft.Extensions.Options;
using PrepodPortal.Common.Configurations;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class ChangeAvatarDtoValidator : AbstractValidator<ChangeUserAvatarDto>
{
    public ChangeAvatarDtoValidator(
        IUserService userService,
        IOptions<AvatarImageConfiguration> avatarImageConfigOptions)
    {
        var avatarImageConfig = avatarImageConfigOptions.Value;
        RuleFor(x => x.UserId)
            .NotEmpty()
            .MustFindUserById(userService);

        RuleFor(x => x.AvatarImageFile)
            .NotEmpty()
            .MustBeOfSpecifiedFileTypes(avatarImageConfig.AllowedFileTypes, "AvatarImageFile")
            .MaximumFileSize(avatarImageConfig.MaximumFileSizeKilobytes, "AvatarImageFile");
    }
}