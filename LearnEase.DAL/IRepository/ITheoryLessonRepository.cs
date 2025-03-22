using LearnEase_Api.Entity;

namespace LearnEase.Repository.IRepository
{
    public interface ITheoryLessonRepository
    {
        Task<TheoryLesson> GetTheoryByLessonId(Guid lessonId);
    }
}
