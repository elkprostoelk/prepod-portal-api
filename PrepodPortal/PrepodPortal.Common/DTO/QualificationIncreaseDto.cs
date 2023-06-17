namespace PrepodPortal.Common.DTO
{
    public class QualificationIncreaseDto
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string OrderNumber { get; set; }

        public string InternshipTheme { get; set; }

        public string Organization { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UserId { get; set; }
    }
}
