using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class NewMonographDto
{
    public string Title { get; set; }
    
    public string? PublishedLocation { get; set; }
    
    public int? PublishedYear { get; set; }
    
    public int TotalPagesCount { get; set; }
    
    public int AuthorPagesCount { get; set; }
    
    public int? TotalPrintedPageCount { get; set; }
    
    public int? PrintedAuthorPagesCount { get; set; }
    
    public ICollection<string> AuthorsIds { get; set; }
    
    public string? PublisherTitle { get; set; }
    
    public string? GryphGiven { get; set; }
    
    public MonographType MonographType { get; set; }

    public string? Isbn { get; set; }
}