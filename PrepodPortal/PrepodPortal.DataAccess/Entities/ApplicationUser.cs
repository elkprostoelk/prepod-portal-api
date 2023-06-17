using Microsoft.AspNetCore.Identity;
using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    
    public string? Town { get; set; }
    
    public string? HomeTown { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public string? AcademicTitle { get; set; }
    
    public string? ScienceDegree { get; set; }
    
    public string? WorkplaceLocation { get; set; }
    
    public string? WorkplacePosition { get; set; }
    
    public long DepartmentId { get; set; }
    
    public string AvatarImagePath { get; set; }
    
    public ICollection<ScientometricDbProfile>? ScientometricDbProfiles { get; set; }
    
    public ICollection<AcademicDegree>? AcademicDegrees { get; set; }
    
    public ICollection<Education> Educations { get; set; }
    
    public ICollection<Publication> Publications { get; set; }
    
    public ICollection<UserPublication> UserPublications { get; set; }
    
    public ICollection<ResearchWork> ResearchWorks { get; set; }
    
    public ICollection<DissertationDefense> DissertationDefenses { get; set; }

    public ICollection<QualificationIncrease> QualificationIncreases { get; set; }

    public ICollection<Subject> Subjects { get; set; }

    public Department? Department { get; set; }
}