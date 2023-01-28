using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class NewArticleDtoValidator : AbstractValidator<NewArticleDto>
{
    public NewArticleDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(dto => dto.AuthorsIds)
            .NotEmpty()
            .MustFindUsersByIds(userService);

        RuleFor(dto => dto.ArticleType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(dto => dto.PublishedLocation)
            .MaximumLength(100);
        
        RuleFor(dto => dto.TotalPagesCount)
            .NotEmpty();
        
        RuleFor(dto => dto.AuthorPagesCount)
            .NotEmpty();
        
        RuleFor(dto => dto.Issn)
            .MaximumLength(100);
        
        RuleFor(dto => dto.Issue)
            .MaximumLength(10);
        
        RuleFor(dto => dto.Tome)
            .MaximumLength(10);

        RuleFor(dto => dto.Number)
            .MaximumLength(10);
        
        RuleFor(dto => dto.EditionName)
            .MaximumLength(50);

        RuleFor(dto => dto.PageNumbers)
            .MaximumLength(20);

        RuleFor(dto => dto.Url)
            .MaximumLength(100);
        
        RuleFor(dto => dto.ScientometricDb)
            .MaximumLength(50);
    }
}