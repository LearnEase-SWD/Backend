using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
    public class TheoryLessonRepository : ITheoryLessonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TheoryLessonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TheoryLesson> GetTheoryByLessonId(Guid lessonId)
        {
            return await _dbContext.TheoryLessons.FirstOrDefaultAsync(theory => theory.LessonID == lessonId);
        }
    }
}
