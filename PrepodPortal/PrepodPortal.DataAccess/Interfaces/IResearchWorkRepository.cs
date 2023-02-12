using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IResearchWorkRepository
{
    Task<bool> AddAsync(ResearchWork researchWork);
    Task<ResearchWork?> GetAsync(long id);
    Task<bool> RemoveAsync(ResearchWork researchWork);
}