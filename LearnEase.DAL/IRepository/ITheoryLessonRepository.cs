using LearnEase.Core.Entities;

namespace LearnEase.Repository.IRepository
{
    public interface ITheoryLessonRepository
    {
        Task<TheoryLesson> GetTheoryByLessonId(Guid lessonId);
    }
}
