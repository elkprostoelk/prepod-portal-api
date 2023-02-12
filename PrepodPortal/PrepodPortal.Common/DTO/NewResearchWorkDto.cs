using PrepodPortal.Common.Enums;

namespace PrepodPortal.Common.DTO;

public class NewResearchWorkDto
{
    public string Title { get; set; }
    
    public string StateRegisterNumber { get; set; }
    
    public ResearchWorkType Type { get; set; }
    
    public string TitleAndContentOfPerformedStage { get; set; }
    
    public string ObtainedScientificResult { get; set; }
    
    public string NoveltyOfScientificResult { get; set; }
    
    public string PracticalResultsValue { get; set; }
        
    public DateTime HeldFrom { get; set; }
        
    public DateTime HeldTo { get; set; }
        
    public long DepartmentId { get; set; }
        
    public ICollection<long>? PublicationIds { get; set; }

    public ICollection<string> PerformerIds { get; set; }
}