namespace PrepodPortal.Common.DTO
{
    public class ArticleDto : PublicationDto
    {
        public string ArticleType { get; set; }

        public string? EditionName { get; set; }

        public string? Number { get; set; }

        public string? Issue { get; set; }

        public string? Tome { get; set; }

        public string? PageNumbers { get; set; }

        public string? Issn { get; set; }

        public string? Url { get; set; }

        public string? ScientometricDb { get; set; }

        public float? SnipIndex { get; set; }
    }
}
