using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator(
        UserManager<ApplicationUser> userManager,
        IUserService userService)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(dto => dto.UserId)
            .NotEmpty()
            .MustFindUserById(userService);

        RuleFor(dto => dto.CurrentPassword)
            .NotEmpty()
            .Length(8, 50)
            .MustAsync(async (dto, password, token) =>
            {
                var user = await userManager.FindByIdAsync(dto.UserId);
                return await userManager.CheckPasswordAsync(user, password);
            }).WithMessage("Current password is wrong!");

        RuleFor(dto => dto.NewPassword)
            .NotEmpty()
            .Length(8, 50)
            .NotEqual(dto => dto.CurrentPassword);
        
        RuleFor(dto => dto.ConfirmNewPassword)
            .NotEmpty()
            .Length(8, 50)
            .Equal(dto => dto.NewPassword);
    }
}