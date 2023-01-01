using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class UserProfile
{
    public long Id { get; set; }
    
    public string UserId { get; set; }
    
    public string Name { get; set; }
    
    public string? Town { get; set; }
    
    public string? HomeTown { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateOnly? BirthDate { get; set; }
    
    public string? AcademicTitle { get; set; }
    
    public string? ScienceDegree { get; set; }
    
    public long DepartmentId { get; set; }
    
    public string AvatarImagePath { get; set; }
    
    public ICollection<ScientometricDbProfile>? ScientometricDbProfiles { get; set; }
    
    public ApplicationUser? User { get; set; }
    
    public Department? Department { get; set; }
}