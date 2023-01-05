using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class AcademicDegreeDto
{
    public long Id { get; set; }
    
    public AcademicDegreeGain Type { get; set; }
    
    public DateTime ReceiveDiplomaDate { get; set; }
    
    public string DiplomaNumber { get; set; }
    
    public string UserId { get; set; }
}