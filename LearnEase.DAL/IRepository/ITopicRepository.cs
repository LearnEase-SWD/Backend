using LearnEase.Core.Models.Reponse;

namespace LearnEase.Repository.IRepository
{
	public interface ITopicRepository
	{
		Task<TopicResponse> GetCourseByTopicAsync(Guid topicId, int pageIndex, int pageSize);
	}
}
