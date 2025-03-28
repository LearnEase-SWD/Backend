using LearnEase.Core;
using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
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

		public async Task<BasePaginatedList<Lesson>> GetLessonsByCourseId(Guid courseId, int pageIndex, int pageSize)
		{
			if(pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 1;

			var query = _context.Lessons.Where(lesson => lesson.CourseID == courseId);

			var totalCount = await query.CountAsync();

			var items = await query
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new BasePaginatedList<Lesson>(items, totalCount, pageIndex, pageSize);
		}
	}
}
