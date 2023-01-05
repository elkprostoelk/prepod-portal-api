using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IAcademicDegreeService
{
    Task<bool> AddAcademicDegreeAsync(NewAcademicDegreeDto newAcademicDegreeDto);
}