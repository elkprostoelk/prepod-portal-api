using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class DissertationDefense
{
    public long Id { get; set; }

    public DissertationDefenseType Type { get; set; }

    public string Theme { get; set; }

    public string CipherAndSpecialty { get; set; }

    public DateTime? DefenseDate { get; set; }

    public DateTime? ReceiveDiplomaDate { get; set; }

    public string DiplomaNumber { get; set; }

    public string? DefenseLocationAndWhoAssignedBy { get; set; }

    public string ScientificDirector { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}