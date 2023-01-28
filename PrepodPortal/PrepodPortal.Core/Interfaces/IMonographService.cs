using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IMonographService
{
    Task<bool> AddMonographAsync(NewMonographDto newMonographDto);
}