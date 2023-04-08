using System.ComponentModel.DataAnnotations;

namespace PrepodPortal.Common.Enums;

public enum DissertationDefenseType
{
    [Display(Name = "Доктор наук")]
    Ph_D = 1,
    [Display(Name = "Кандидат наук")]
    PhD
}