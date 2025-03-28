﻿using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Core.Base;

namespace LearnEase.Core.IServices
{
	public interface IUserCourseService
	{
		Task<BaseResponse<IEnumerable<UserCourseResponse>>> GetUserCoursesAsync(int pageIndex, int pageSize);
		Task<BaseResponse<UserCourseResponse>> GetUserCourseByIdAsync(Guid userCourseId);
		Task<BaseResponse<UserCourseResponse>> GetUserCourseByIdCourseAsync(Guid courseId, Guid userId); // Sửa kiểu userId thành Guid
		Task<BaseResponse<IEnumerable<CourseResponse>>> GetCoursesByUserAsync(string userId);
		Task<BaseResponse<bool>> CreateUserCourseAsync(UserCourseRequest userCourseRequest);
		Task<BaseResponse<bool>> UpdateUserCourseAsync(Guid userCourseId, UserCourseRequest userCourseRequest);
		Task<BaseResponse<bool>> DeleteUserCourseAsync(Guid userCourseId);
	}
}
