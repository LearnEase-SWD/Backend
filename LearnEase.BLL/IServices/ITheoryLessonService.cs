using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
    public interface ITheoryLessonService
    {
        Task<BaseResponse<IEnumerable<TheoryLesson>>> GetTheoryLessonsAsync(int pageIndex, int pageSize);
        Task<BaseResponse<TheoryLesson>> GetTheoryLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateTheoryLessonAsync(TheoryLessonCreateRequest theoryLessonRequest);
        Task<BaseResponse<bool>> UpdateTheoryLessonAsync(Guid id, TheoryLessonCreateRequest request);
        Task<BaseResponse<bool>> DeleteTheoryLessonAsync(Guid id);
    }
}
