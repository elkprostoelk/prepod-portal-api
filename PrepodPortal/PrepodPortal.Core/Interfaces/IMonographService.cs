using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IMonographService
{
    Task<ServiceResult<long>> AddMonographAsync(NewMonographDto newMonographDto);
}