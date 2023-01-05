using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewAcademicDegreeDtoValidator : AbstractValidator<NewAcademicDegreeDto>
{
    public NewAcademicDegreeDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty()
            .MustFindUserById(userService);

        RuleFor(dto => dto.Type)
            .IsInEnum()
            .NotNull();

        RuleFor(dto => dto.ReceiveDiplomaDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now);

        RuleFor(dto => dto.DiplomaNumber)
            .NotEmpty()
            .MaximumLength(15);
    }
}