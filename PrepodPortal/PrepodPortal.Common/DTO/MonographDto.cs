namespace PrepodPortal.Common.DTO
{
    public class MonographDto : PublicationDto
    {
        public string? PublisherTitle { get; set; }

        public string? GryphGiven { get; set; }

        public string MonographType { get; set; }

        public string? Isbn { get; set; }
    }
}
