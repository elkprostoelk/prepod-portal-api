using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class NewAcademicDegreeDto
{
    public AcademicDegreeGain Type { get; set; }
    
    public DateTime ReceiveDiplomaDate { get; set; }
    
    public string DiplomaNumber { get; set; }
    
    public string UserId { get; set; }
}