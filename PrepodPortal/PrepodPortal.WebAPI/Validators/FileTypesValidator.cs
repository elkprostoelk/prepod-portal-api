using FluentValidation;
using FluentValidation.Validators;

namespace PrepodPortal.WebAPI.Validators;

public class FileTypesValidator<T> : PropertyValidator<T, IFormFile>
{
    private readonly string[] _extensions;

    public FileTypesValidator(string[] extensions) =>
        _extensions = extensions;

    public override bool IsValid(ValidationContext<T> context, IFormFile value) =>
        _extensions.Contains(Path.GetExtension(value.FileName)[1..]);

    public override string Name => "FileTypesValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        $"File must be only of types {String.Concat(", ", _extensions)}";
}