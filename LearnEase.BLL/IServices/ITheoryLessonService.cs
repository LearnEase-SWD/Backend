using LearnEase.Core.Base;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
    public interface ITheoryLessonService
    {
        Task<BaseResponse<IEnumerable<TheoryLessonResponse>>> GetTheoryLessonsAsync(int pageIndex, int pageSize);
        Task<BaseResponse<TheoryLessonResponse>> GetTheoryLessonByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateTheoryLessonAsync(TheoryLessonCreateRequest theoryLessonRequest);
        Task<BaseResponse<bool>> UpdateTheoryLessonAsync(Guid id, TheoryLessonCreateRequest request);
        Task<BaseResponse<bool>> DeleteTheoryLessonAsync(Guid id);
        Task<BaseResponse<bool>> MarkTheoryLessonAsCompletedAsync(string userId, Guid theoryLessonId);

    }
}
