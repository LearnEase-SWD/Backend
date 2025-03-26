using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lesson> GetLessonByCourseId(Guid courseId)
        {
            return await _context.Lessons.FirstOrDefaultAsync(lesson => lesson.CourseID == courseId);
        }
    }
}
