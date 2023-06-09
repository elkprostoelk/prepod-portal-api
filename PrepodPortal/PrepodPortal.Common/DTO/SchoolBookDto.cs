namespace PrepodPortal.Common.DTO
{
    public class SchoolBookDto : PublicationDto
    {
        public string SchoolBookType { get; set; }

        public string GryphType { get; set; }

        public string? Isbn { get; set; }

        public string OrderNum { get; set; }

        public DateTime OrderDate { get; set; }

        public string? PublisherTitle { get; set; }
    }
}
