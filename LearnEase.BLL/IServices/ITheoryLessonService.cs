using LearnEase.Core.Base;
using LearnEase.Core.Models.Request;
using LearnEase_Api.Entity;

namespace LearnEase.Service.IServices
{
    public interface ITheoryLessonService
    {
        Task<BaseResponse<IEnumerable<TheoryLesson>>> GetTheoryLessonsAsync(int pageIndex, int pageSize);
        Task<BaseResponse<TheoryLesson>> GetTheoryLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateTheoryLessonAsync(TheoryLessonCreationRequest theoryLessonRequest);
        Task<BaseResponse<bool>> UpdateTheoryLessonAsync(Guid id, TheoryLessonCreationRequest request);
        Task<BaseResponse<bool>> DeleteTheoryLessonAsync(Guid id);
    }
}
