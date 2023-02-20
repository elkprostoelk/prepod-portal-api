using FluentValidation;
using FluentValidation.Validators;

namespace PrepodPortal.WebAPI.Validators;

public class FileSizeValidator<T> : PropertyValidator<T, IFormFile>
{
    private readonly int _maxSizeKilobytes;

    public FileSizeValidator(int maxSizeKilobytes) =>
        _maxSizeKilobytes = maxSizeKilobytes;

    public override bool IsValid(ValidationContext<T> context, IFormFile value) =>
        value.Length <= _maxSizeKilobytes;

    public override string Name => "FileSizeValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        $"File must be less than {_maxSizeKilobytes}";
}