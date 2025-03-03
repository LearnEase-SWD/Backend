using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface ILessonService
    {

        Task<IEnumerable<Lesson>> GetAllLessonsAsync();
        Task<Lesson?> GetLessonByIdAsync(Guid id);
        Task CreateLessonAsync(Lesson lesson);
        Task<bool> UpdateLessonAsync(Guid id, Lesson lesson);
        Task<bool> DeleteLessonAsync(Guid id);

    }
}
