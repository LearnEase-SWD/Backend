using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;

namespace LearnEase.Service.IServices
{
	public interface ITopicService
	{
		Task<BaseResponse<IEnumerable<Topic>>> GetTopicsAsync(int pageIndex, int pageSize);
		Task<BaseResponse<TopicResponse>> GetCoursesByTopicAsync(Guid topicId, int pageIndex, int pageSize);
		Task<BaseResponse<Topic>> GetTopicByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateTopicAsync(string topicName);
		Task<BaseResponse<bool>> UpdateTopicAsync(Guid id, string topicName);
		Task<BaseResponse<bool>> DeleteTopicAsync(Guid id);
	}
}
