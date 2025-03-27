﻿using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
    public interface IVideoLessonService
    {
        Task<BaseResponse<IEnumerable<VideoLesson>>> GetVideoLessonsAsync(int pageIndex, int pageSize);
        Task<BaseResponse<VideoLesson>> GetVideoLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateVideoLessonAsync(VideoLessonCreationRequest videoRequest);
        Task<BaseResponse<bool>> UpdateVideoLessonAsync(Guid id, VideoLessonCreationRequest request);
        Task<BaseResponse<bool>> DeleteVideoLessonAsync(Guid id);
    }
}
