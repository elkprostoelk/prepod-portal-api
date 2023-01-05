using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class AcademicDegree
{
    public long Id { get; set; }
    
    public AcademicDegreeGain Type { get; set; }
    
    public DateTime ReceiveDiplomaDate { get; set; }
    
    public string DiplomaNumber { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser? User { get; set; }
}