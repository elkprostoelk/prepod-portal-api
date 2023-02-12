using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewResearchWorkDtoValidator : AbstractValidator<NewResearchWorkDto>
{
    public NewResearchWorkDtoValidator(
        IUserService userService,
        IDepartmentService departmentService)
    {
        RuleFor(dto => dto.PerformerIds)
            .NotEmpty()
            .MustFindUsersByIds(userService);

        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(dto => dto.DepartmentId)
            .NotEmpty()
            .MustFindDepartmentById(departmentService);

        RuleFor(dto => dto.Type)
            .NotNull()
            .IsInEnum();

        RuleFor(dto => dto.HeldFrom)
            .NotEmpty()
            .LessThan(dto => dto.HeldTo);

        RuleFor(dto => dto.HeldTo)
            .NotEmpty()
            .GreaterThan(dto => dto.HeldFrom);

        RuleFor(dto => dto.ObtainedScientificResult)
            .NotNull()
            .MaximumLength(500);

        RuleFor(dto => dto.PracticalResultsValue)
            .NotNull()
            .MaximumLength(500);

        RuleFor(dto => dto.StateRegisterNumber)
            .NotNull()
            .MaximumLength(20);

        RuleFor(dto => dto.NoveltyOfScientificResult)
            .NotNull()
            .MaximumLength(500);

        RuleFor(dto => dto.TitleAndContentOfPerformedStage)
            .NotNull()
            .MaximumLength(500);
    }
}