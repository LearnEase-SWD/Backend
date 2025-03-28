using LearnEase.Core.Models.Reponse;

namespace LearnEase.Repository.IRepository
{
	public interface ITopicRepository
	{
		Task<TopicResponse> GetCourseByTopic(Guid topicId, int pageIndex, int pageSize);
	}
}
