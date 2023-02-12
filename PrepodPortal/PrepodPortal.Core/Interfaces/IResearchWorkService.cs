using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IResearchWorkService
{
    Task<ServiceResult<long>> AddResearchWorkAsync(NewResearchWorkDto newResearchWorkDto);
    Task<ResearchWorkDto?> GetAsync(long id);
    Task<ServiceResult> DeleteAsync(long id);
}