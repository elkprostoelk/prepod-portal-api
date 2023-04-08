using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class DissertationDefenseDto
{
    public long Id { get; set; }

    public string Type { get; set; }

    public string Theme { get; set; }

    public string CipherAndSpecialty { get; set; }

    public DateTime? DefenseDate { get; set; }

    public DateTime? ReceiveDiplomaDate { get; set; }

    public string DiplomaNumber { get; set; }

    public string? DefenseLocationAndWhoAssignedBy { get; set; }

    public string ScientificDirector { get; set; }
    
    public string UserId { get; set; }
}