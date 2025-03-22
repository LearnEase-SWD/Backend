using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface ICourseService
    {

        Task<IEnumerable<Course>> GetCoursesAsync(int pageIndex, int pageSize);
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<Course> CreateCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Guid id, Course course);
        Task<bool> DeleteCourseAsync(Guid id);

    }
}
