namespace PrepodPortal.DataAccess.Entities;

public abstract class Publication
{
    public long Id { get; set; }
    
    public string PublicationType { get; set; }
    
    public string Title { get; set; }
    
    public string? PublishedLocation { get; set; }
    
    public int? PublishedYear { get; set; }
    
    public int TotalPagesCount { get; set; }
    
    public int AuthorPagesCount { get; set; }
    
    public int? TotalPrintedPageCount { get; set; }
    
    public int? PrintedAuthorPagesCount { get; set; }
    
    public ICollection<ApplicationUser>? Authors { get; set; }
    
    public ICollection<UserPublication>? UserPublications { get; set; }
}