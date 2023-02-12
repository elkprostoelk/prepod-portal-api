using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewMonographDtoValidator : AbstractValidator<NewMonographDto>
{
    public NewMonographDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(dto => dto.AuthorsIds)
            .NotEmpty()
            .MustFindUsersByIds(userService);

        RuleFor(dto => dto.MonographType)
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
        
        RuleFor(dto => dto.GryphGiven)
            .MaximumLength(100);
        
        RuleFor(dto => dto.PublisherTitle)
            .MaximumLength(100);

        RuleFor(dto => dto.PublishedYear)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.Year);
    }
}