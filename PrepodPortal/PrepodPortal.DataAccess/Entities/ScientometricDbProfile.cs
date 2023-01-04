namespace PrepodPortal.DataAccess.Entities;

public class ScientometricDbProfile
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string ProfileLink { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser? User { get; set; }
}