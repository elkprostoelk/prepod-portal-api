using FluentValidation;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Validators;

public class AddPublicationsWithCsvDtoValidator : AbstractValidator<AddPublicationsWithCsvDto>
{
    public AddPublicationsWithCsvDtoValidator(IUserService userService)
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty()
            .MustFindUserById(userService);

        RuleFor(dto => dto.CsvFile)
            .NotEmpty()
            .MustBeOfSpecifiedFileTypes("csv")
            .MaximumFileSize(5242880);
    }
}