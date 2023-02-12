namespace PrepodPortal.DataAccess.Entities;

public class UserResearchWork
{
    public string UserId { get; set; }
    
    public long ResearchWorkId { get; set; }
    
    public ApplicationUser? User { get; set; }
    
    public ResearchWork? ResearchWork { get; set; }
}