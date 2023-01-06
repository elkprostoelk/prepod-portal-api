namespace PrepodPortal.Common.DTO;

public class NewEducationDto
{
    public string UserId { get; set; }
    
    public string Institution { get; set; }

    public ushort StartYear { get; set; }
    
    public ushort EndYear { get; set; }
    
    public string QualificationByDiploma { get; set; }
    
    public string Specialty { get; set; }
}