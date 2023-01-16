namespace PrepodPortal.DataAccess.Entities;

public class UserPublication
{
    public long PublicationId { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser? User { get; set; }
    
    public Publication? Publication { get; set; }
}