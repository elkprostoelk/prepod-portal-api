using PrepodPortal.Common.Enums;

namespace PrepodPortal.DataAccess.Entities;

public class SchoolBook : Publication
{
    public SchoolBookType SchoolBookType { get; set; }
    
    public MonGryphType GryphType { get; set; }
    
    public string? Isbn { get; set; }
    
    public string OrderNum { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public string? PublisherTitle { get; set; }
}