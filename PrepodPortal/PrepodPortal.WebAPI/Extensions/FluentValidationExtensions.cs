using FluentValidation;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.WebAPI.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> MustFindUserById<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IUserService userService) => 
        ruleBuilder.MustAsync(async (userId, token) =>
                await userService.UserExistsAsync(userId, token))
            .WithMessage("User with specified ID does not exist!");

    public static IRuleBuilder<T, IEnumerable<string>> MustFindUsersByIds<T>(
        this IRuleBuilder<T, IEnumerable<string>> ruleBuilder,
        IUserService userService) =>
        ruleBuilder.MustAsync(async (ids, token) =>
        {
            foreach (var id in ids)
            {
                if (!await userService.UserExistsAsync(id, token))
                {
                    return false;
                }
            }

            return true;
        });

    public static IRuleBuilder<T, IFormFile> MustBeOfSpecifiedFileTypes<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        params string[] extensions) =>
        ruleBuilder.Must((dto, file) =>
                file is null || extensions.Contains(Path.GetExtension(file.FileName)[1..].ToLower()))
            .WithMessage($"File must be {String.Join(",", extensions)} only!");

    public static IRuleBuilder<T, IFormFile> MaximumFileSize<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        int maxFileSizeInKilobytes) =>
        ruleBuilder.Must((dto, file) =>
                file is null || file.Length <= maxFileSizeInKilobytes)
            .WithMessage($"File must be less than {maxFileSizeInKilobytes / 1024} KB!");
}