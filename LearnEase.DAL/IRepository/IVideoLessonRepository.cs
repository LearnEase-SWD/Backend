using LearnEase_Api.Entity;

namespace LearnEase.Repository.IRepository
{
	public interface IVideoLessonRepository
	{
		Task<VideoLesson> GetVideoByLessonId(Guid lessonId); 
	}
}
