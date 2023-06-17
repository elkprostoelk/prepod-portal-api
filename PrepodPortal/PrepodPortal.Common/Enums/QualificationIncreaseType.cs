using System.ComponentModel.DataAnnotations;

namespace PrepodPortal.Common.Enums
{
    public enum QualificationIncreaseType
    {
        [Display(Name = "Стажування у ВНЗ України")]
        InternshipInUkranianUniversity,

        [Display(Name = "Стажування за кордоном")]
        InternshipOverseas,

        [Display(Name = "Курси підвищення кваліфікації")]
        QualificationIncreaseCourses,

        [Display(Name = "Творчі відпустки")]
        CreativeVacations
    }
}