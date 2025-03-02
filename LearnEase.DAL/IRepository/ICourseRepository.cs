using LearnEase.Core;
using LearnEase.Repository.Repository;
using LearnEase_Api.Entity;

namespace LearnEase.Repository.IRepository
{
    public interface ICourseRepository: IGenericRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(Guid id);
    }
}
