using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IAcademicDegreeRepository
{
    Task<bool> AddAsync(AcademicDegree academicDegree);
    Task<AcademicDegree?> GetAsync(long id);
    Task<bool> RemoveAsync(AcademicDegree academicDegree);
}