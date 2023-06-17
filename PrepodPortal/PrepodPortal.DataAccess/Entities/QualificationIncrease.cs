using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities
{
    public class QualificationIncrease
    {
        public long Id { get; set; }

        public QualificationIncreaseType Type { get; set; }

        public string OrderNumber { get; set; }

        public string InternshipTheme { get; set; }

        public string Organization { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
