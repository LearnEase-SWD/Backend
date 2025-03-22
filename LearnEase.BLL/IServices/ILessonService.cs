﻿using LearnEase.Core.Base;
using LearnEase.Core.Models.Request;
using LearnEase_Api.Entity;

namespace LearnEase.Service.IServices
{
    public interface ILessonService
    {
        Task<BaseResponse<IEnumerable<Lesson>>> GetLessonsAsync(int index, int pageSize);
        Task<BaseResponse<Lesson>> GetLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateLessonAsync(LessonCreationRequest lesson);
        Task<BaseResponse<bool>> UpdateLessonAsync(Guid id, LessonCreationRequest lesson);
        Task<BaseResponse<bool>> DeleteLessonAsync(Guid id);
    }
}
