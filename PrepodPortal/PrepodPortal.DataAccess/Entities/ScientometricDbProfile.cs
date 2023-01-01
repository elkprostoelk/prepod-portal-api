namespace PrepodPortal.DataAccess.Entities;

public class ScientometricDbProfile
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string ProfileLink { get; set; }
    
    public long UserProfileId { get; set; }
    
    public UserProfile? UserProfile { get; set; }
}