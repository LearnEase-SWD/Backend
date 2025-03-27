using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Request;
namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface ICourseService
    {
		Task<BaseResponse<IEnumerable<Course>>> GetCoursesAsync(int pageIndex, int pageSize);
		Task<BaseResponse<Course>> GetCourseByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateCourseAsync(CourseRequest courseRequest);
		Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, CourseRequest course);
		Task<BaseResponse<bool>> DeleteCourseAsync(Guid id);
	}
}
