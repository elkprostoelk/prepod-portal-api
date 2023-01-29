using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface ILectureThesesService
{
    Task<bool> AddLectureThesesAsync(NewLectureThesesDto newLectureThesesDto);
}