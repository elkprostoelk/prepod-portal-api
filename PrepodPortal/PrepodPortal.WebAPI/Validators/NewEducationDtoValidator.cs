using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewEducationDtoValidator : AbstractValidator<NewEducationDto>
{
    public NewEducationDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty()
            .MustFindUserById(userService);

        RuleFor(dto => dto.Institution)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(dto => dto.Specialty)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(dto => dto.QualificationByDiploma)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(dto => dto.StartYear)
            .NotEmpty()
            .LessThanOrEqualTo((ushort)DateTime.Now.Year);
        
        RuleFor(dto => dto.EndYear)
            .NotEmpty()
            .LessThanOrEqualTo((ushort)DateTime.Now.Year)
            .GreaterThan(dto => dto.StartYear);
    }
}