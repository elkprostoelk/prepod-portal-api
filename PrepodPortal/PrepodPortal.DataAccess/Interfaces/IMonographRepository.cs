using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IMonographRepository
{
    Task<bool> AddAsync(Monograph monograph);
}