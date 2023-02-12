using System.Xml;
using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewLectureThesesDtoValidator : AbstractValidator<NewLectureThesesDto>
{
    public NewLectureThesesDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(dto => dto.AuthorsIds)
            .NotEmpty()
            .MustFindUsersByIds(userService);

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

        RuleFor(dto => dto.PublishedYear)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.Year);
        
        RuleFor(dto => dto.Issue)
            .MaximumLength(10);
        
        RuleFor(dto => dto.Tome)
            .MaximumLength(10);

        RuleFor(dto => dto.Number)
            .MaximumLength(10);
        
        RuleFor(dto => dto.EditionTitle)
            .MaximumLength(50);

        RuleFor(dto => dto.PageNumbers)
            .MaximumLength(20);

        RuleFor(dto => dto.Url)
            .MaximumLength(100);

        RuleFor(dto => dto.OrderNumber)
            .MaximumLength(20);
    }
}