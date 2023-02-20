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
        string[] extensions,
        string propertyName) =>
        ruleBuilder.Must((dto, file) =>
                extensions.Contains(Path.GetExtension(file.FileName)[1..].ToLower()))
            .WithMessage($"File must be {String.Join(",", extensions)} only!")
            .WithName(propertyName);

    public static IRuleBuilder<T, IFormFile> MaximumFileSize<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        int maxFileSizeInKilobytes,
        string propertyName) =>
        ruleBuilder.Must((dto, file) =>
                file.Length <= maxFileSizeInKilobytes * 1024)
            .WithMessage($"File must be less than {maxFileSizeInKilobytes} KB!")
            .WithName(propertyName);

    public static IRuleBuilder<T, long> MustFindDepartmentById<T>(
        this IRuleBuilder<T, long> ruleBuilder,
        IDepartmentService departmentService) =>
        ruleBuilder.MustAsync(async (departmentId, token) =>
            await departmentService.ExistsAsync(departmentId, token))
            .WithMessage("Department does not exist!");
}