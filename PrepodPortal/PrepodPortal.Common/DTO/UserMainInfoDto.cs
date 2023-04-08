namespace PrepodPortal.Common.DTO;

public class UserMainInfoDto
{
    public string? Town { get; set; }
    
    public string? HomeTown { get; set; }
    
    public string Gender { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public string? AcademicTitle { get; set; }
    
    public string? ScienceDegree { get; set; }
    
    public string? WorkplaceLocation { get; set; }
    
    public string? WorkplacePosition { get; set; }
    
    public string Department { get; set; }

    public ICollection<ScientometricDbProfileDto> ScientometricDbProfiles { get; set; }
        = new List<ScientometricDbProfileDto>();

    public ICollection<AcademicDegreeDto> AcademicDegrees { get; set; }
        = new List<AcademicDegreeDto>();

    public ICollection<EducationDto> Educations { get; set; }
        = new List<EducationDto>();

    public ICollection<DissertationDefenseDto> DissertationDefenses { get; set; }
        = new List<DissertationDefenseDto>();
}