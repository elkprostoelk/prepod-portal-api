using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IEducationService
{
    Task<ServiceResult<long>> AddAsync(NewEducationDto newEducationDto);
    Task<EducationDto?> GetAsync(long id);
    Task<ServiceResult> DeleteAsync(long id);
}