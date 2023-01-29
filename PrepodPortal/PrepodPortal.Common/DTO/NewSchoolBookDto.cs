using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class NewSchoolBookDto
{
    public string Title { get; set; }
    
    public string? PublishedLocation { get; set; }
    
    public int? PublishedYear { get; set; }
    
    public int TotalPagesCount { get; set; }
    
    public int AuthorPagesCount { get; set; }
    
    public int? TotalPrintedPageCount { get; set; }
    
    public int? PrintedAuthorPagesCount { get; set; }
    
    public ICollection<string> AuthorsIds { get; set; }
    
    public SchoolBookType SchoolBookType { get; set; }
    
    public MonGryphType GryphType { get; set; }
    
    public string? Isbn { get; set; }
    
    public string OrderNum { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public string? PublisherTitle { get; set; }
}