using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface ISchoolBookService
{
    Task<ServiceResult<long>> AddSchoolBookAsync(NewSchoolBookDto newSchoolBookDto);
}