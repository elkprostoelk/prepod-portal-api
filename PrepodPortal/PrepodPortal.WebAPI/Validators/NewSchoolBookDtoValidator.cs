using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewSchoolBookDtoValidator : AbstractValidator<NewSchoolBookDto>
{
    public NewSchoolBookDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(dto => dto.AuthorsIds)
            .NotEmpty()
            .MustFindUsersByIds(userService);

        RuleFor(dto => dto.SchoolBookType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(dto => dto.PublishedLocation)
            .MaximumLength(100);
        
        RuleFor(dto => dto.TotalPagesCount)
            .NotEmpty();
        
        RuleFor(dto => dto.AuthorPagesCount)
            .NotEmpty();
        
        RuleFor(dto => dto.TotalPrintedPageCount)
            .GreaterThan(0);
        
        RuleFor(dto => dto.PrintedAuthorPagesCount)
            .GreaterThan(0);
        
        RuleFor(dto => dto.Isbn)
            .MaximumLength(20);

        RuleFor(dto => dto.GryphType)
            .NotNull()
            .IsInEnum();
        
        RuleFor(dto => dto.PublisherTitle)
            .MaximumLength(100);

        RuleFor(dto => dto.PublishedYear)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.Year);

        RuleFor(dto => dto.OrderDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now);

        RuleFor(dto => dto.OrderNum)
            .NotEmpty()
            .MaximumLength(20);
    }
}