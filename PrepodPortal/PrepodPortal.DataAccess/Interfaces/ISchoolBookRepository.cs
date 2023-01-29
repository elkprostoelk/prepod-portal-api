using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface ISchoolBookRepository
{
    Task<bool> AddAsync(SchoolBook schoolBook);
}