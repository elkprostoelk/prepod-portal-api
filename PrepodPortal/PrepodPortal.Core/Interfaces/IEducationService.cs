using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IEducationService
{
    Task<bool> AddAsync(NewEducationDto newEducationDto);
    Task<EducationDto?> GetAsync(long id);
    Task<bool> DeleteAsync(long id);
}