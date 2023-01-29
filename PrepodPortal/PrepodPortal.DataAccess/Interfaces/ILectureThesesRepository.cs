using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface ILectureThesesRepository
{
    Task<bool> AddAsync(LectureTheses lectureTheses);
}