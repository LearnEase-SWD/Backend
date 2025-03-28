﻿using LearnEase.Core;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
    public interface ILessonService
    {
		Task<BaseResponse<IEnumerable<LessonResponse>>> GetLessonsAsync(int pageIndex, int pageSize);
        Task<BaseResponse<IEnumerable<LessonResponse>>> GetLessonsByCourseIdAsync(Guid courseId, int pageIndex, int pageSize);
		Task<BaseResponse<LessonResponse>> GetLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateLessonAsync(LessonCreateRequest lesson);
        Task<BaseResponse<bool>> UpdateLessonAsync(Guid id, LessonCreateRequest lesson);
        Task<BaseResponse<bool>> DeleteLessonAsync(Guid id);
    }
}
