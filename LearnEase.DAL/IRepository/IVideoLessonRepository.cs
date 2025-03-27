using LearnEase.Core.Entities;

namespace LearnEase.Repository.IRepository
{
	public interface IVideoLessonRepository
	{
		Task<VideoLesson> GetVideoByLessonId(Guid lessonId); 
	}
}
