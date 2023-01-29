namespace PrepodPortal.Common.DTO;

public class NewLectureThesesDto
{
    public string Title { get; set; }
    
    public string? PublishedLocation { get; set; }
    
    public int? PublishedYear { get; set; }
    
    public int TotalPagesCount { get; set; }
    
    public int AuthorPagesCount { get; set; }
    
    public int? TotalPrintedPageCount { get; set; }
    
    public int? PrintedAuthorPagesCount { get; set; }
    
    public ICollection<string> AuthorsIds { get; set; }
    
    public string? EditionTitle { get; set; }
    
    public string? Number { get; set; }
    
    public string? Issue { get; set; }
    
    public string? Tome { get; set; }
    
    public string? PageNumbers { get; set; }
    
    public string? Isbn { get; set; }
    
    public string? OrderNumber { get; set; }
    
    public string? Url { get; set; }
}