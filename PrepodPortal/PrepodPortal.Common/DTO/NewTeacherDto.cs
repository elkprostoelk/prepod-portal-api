namespace PrepodPortal.Common.DTO;

public class NewTeacherDto
{
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public long DepartmentId { get; set; }
    
    public ICollection<NewScientometricDbProfileDto> ScientometricDbProfiles { get; set; }
}