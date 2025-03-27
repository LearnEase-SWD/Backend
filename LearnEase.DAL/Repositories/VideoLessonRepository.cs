using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
	public class VideoLessonRepository : IVideoLessonRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public VideoLessonRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<VideoLesson> GetVideoByLessonId(Guid lessonId) 
			=> await _dbContext.VideoLessons.FirstOrDefaultAsync(video => video.LessonID == lessonId);
	}
}
