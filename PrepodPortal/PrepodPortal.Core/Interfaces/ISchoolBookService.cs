using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface ISchoolBookService
{
    Task<bool> AddSchoolBookAsync(NewSchoolBookDto newSchoolBookDto);
}