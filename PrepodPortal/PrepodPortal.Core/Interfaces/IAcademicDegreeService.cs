using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IAcademicDegreeService
{
    Task<ServiceResult<long>> AddAcademicDegreeAsync(NewAcademicDegreeDto newAcademicDegreeDto);
    Task<AcademicDegreeDto?> GetAsync(long id);
    Task<ServiceResult> DeleteAsync(long id);
}