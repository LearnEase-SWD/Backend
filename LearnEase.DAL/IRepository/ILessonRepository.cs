using LearnEase_Api.Entity;

namespace LearnEase.Repository.IRepository
{
    public interface ILessonRepository
    {
        Task<Lesson> GetLessonByCourseId(Guid courseId);
    }
}
