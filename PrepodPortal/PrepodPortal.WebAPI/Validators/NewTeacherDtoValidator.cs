using FluentValidation;
using PrepodPortal.Common.DTO;

namespace PrepodPortal.WebAPI.Validators;

public class NewTeacherDtoValidator : AbstractValidator<NewTeacherDto>
{
    public NewTeacherDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(dto => dto.DepartmentId)
            .NotEmpty();

        RuleFor(dto => dto.ScientometricDbProfiles)
            .NotEmpty();
    }
}