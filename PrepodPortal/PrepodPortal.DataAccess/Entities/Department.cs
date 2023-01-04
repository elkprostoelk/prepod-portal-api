namespace PrepodPortal.DataAccess.Entities;

public class Department
{
    public long Id { get; set; }
    
    public string Title { get; set; }
    
    public ICollection<ApplicationUser>? Users { get; set; }
}