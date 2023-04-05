using FluentValidation;
using PrepodPortal.Common.DTO;

namespace PrepodPortal.WebAPI.Validators;

public class NewScientometricDbProfileDtoValidator : AbstractValidator<ScientometricDbProfileDto>
{
    public NewScientometricDbProfileDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(dto => dto.ProfileLink)
            .NotEmpty()
            .MaximumLength(200);
    }
}