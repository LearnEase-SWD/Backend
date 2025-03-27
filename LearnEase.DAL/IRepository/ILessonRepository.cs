
using LearnEase.Core.Entities;

namespace LearnEase.Repository.IRepository
{
    public interface ILessonRepository
    {
        Task<Lesson> GetLessonByCourseId(Guid courseId);
    }
}
