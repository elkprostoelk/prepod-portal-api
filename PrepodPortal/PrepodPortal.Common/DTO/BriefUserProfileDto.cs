namespace PrepodPortal.Common.DTO;

public class BriefUserProfileDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Town { get; set; }
    
    public string? AcademicTitle { get; set; }
    
    public string? ScienceDegree { get; set; }
    
    public string DepartmentTitle { get; set; }
    
    public string AvatarImagePath { get; set; }
}