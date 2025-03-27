using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
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
			=> await _dbContext.TheoryLessons.FirstOrDefaultAsync(theory => theory.LessonID == lessonId);

	}
}
