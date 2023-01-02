using FluentValidation;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.WebAPI.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> MustFindUserById<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IUserService userService)
    {
        return ruleBuilder.MustAsync(async (userId, token) =>
                await userService.UserExistsAsync(userId, token))
            .WithMessage("User with specified ID does not exist!");
    }
}