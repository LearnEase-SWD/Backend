using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
    public interface IVideoLessonService
    {
        Task<BaseResponse<IEnumerable<VideoLessonResponse>>> GetVideoLessonsAsync(int pageIndex, int pageSize);
		Task<BaseResponse<VideoLessonResponse>> GetVideoLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateVideoLessonAsync(VideoLessonCreateRequest videoRequest);
        Task<BaseResponse<bool>> UpdateVideoLessonAsync(Guid id, VideoLessonCreateRequest request);
        Task<BaseResponse<bool>> DeleteVideoLessonAsync(Guid id);
        Task<BaseResponse<bool>> MarkVideoLessonAsCompletedAsync(string userId, Guid videoLessonId);
    }
}
