using System.ComponentModel.DataAnnotations;

namespace PrepodPortal.Common.Enums;

public enum AcademicDegreeGain
{
    [Display(Name = "Професор")]
    Professor,
    [Display(Name = "Доцент")]
    AssistantProfessor
}