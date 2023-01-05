using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IAcademicDegreeService
{
    Task<bool> AddAcademicDegreeAsync(NewAcademicDegreeDto newAcademicDegreeDto);
    Task<AcademicDegreeDto?> GetAsync(long id);
    Task<bool> DeleteAsync(long id);
}