using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class Monograph : Publication
{
    public string? PublisherTitle { get; set; }
    
    public string? GryphGiven { get; set; }
    
    public MonographType MonographType { get; set; }

    public string? Isbn { get; set; }
}