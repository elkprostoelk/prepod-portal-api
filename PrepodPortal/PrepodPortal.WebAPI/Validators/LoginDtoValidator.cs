using FluentValidation;
using PrepodPortal.Common.DTO;

namespace PrepodPortal.WebAPI.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(dto => dto.Password)
            .NotEmpty()
            .Length(8, 50);
    }
}