using System.ComponentModel.DataAnnotations;

namespace PrepodPortal.Common.Enums;

public enum Gender
{
    [Display(Name = "Чоловіча")]
    Male,
    [Display(Name = "Жіноча")]
    Female
}