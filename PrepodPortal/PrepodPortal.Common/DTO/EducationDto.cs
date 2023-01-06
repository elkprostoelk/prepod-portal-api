namespace PrepodPortal.Common.DTO;

public class EducationDto
{
    public long Id { get; set; }

    public string UserId { get; set; }
    
    public string Institution { get; set; }

    public ushort StartYear { get; set; }
    
    public ushort EndYear { get; set; }
    
    public string QualificationByDiploma { get; set; }
    
    public string Specialty { get; set; }
}