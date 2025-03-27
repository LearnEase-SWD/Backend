using LearnEase.Core.Base;
using LearnEase.Core.Entities;
namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface ICourseService
    {
		Task<BaseResponse<IEnumerable<Course>>> GetCoursesAsync(int pageIndex, int pageSize);
		Task<BaseResponse<Course>> GetCourseByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateCourseAsync(Course course);
		Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, Course course);
		Task<BaseResponse<bool>> DeleteCourseAsync(Guid id);
	}
}
