using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface ILectureThesesService
{
    Task<ServiceResult<long>> AddLectureThesesAsync(NewLectureThesesDto newLectureThesesDto);
}