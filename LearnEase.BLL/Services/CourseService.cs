using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
	public class CourseService : ICourseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CourseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<IEnumerable<Course>>> GetCoursesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var query = courseRepository.Entities;
				var paginatedResult = await courseRepository.GetPagging(query, pageIndex, pageSize);

				return new BaseResponse<IEnumerable<Course>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					paginatedResult.Items,
					"Lấy danh sách khóa học thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<Course>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<Course>(),
					"Lỗi hệ thống khi lấy danh sách khóa học."
				);
			}
		}

		public async Task<BaseResponse<Course>> GetCourseByIdAsync(Guid id)
		{
			try
			{
				var course = await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
				if (course == null)
					return new BaseResponse<Course>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Khóa học không tồn tại.");

				return new BaseResponse<Course>(StatusCodeHelper.OK, "SUCCESS", course, "Lấy khóa học thành công.");
			}
			catch (Exception ex)
			{
				return new BaseResponse<Course>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy khóa học.");
			}
		}

		public async Task<BaseResponse<bool>> CreateCourseAsync(CourseRequest courseRequest)
		{
			if (courseRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu khóa học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var course = _mapper.Map<Course>(courseRequest);
				course.CreatedAt = DateTime.Now;
				course.UpdatedAt = DateTime.Now;
				await _unitOfWork.GetRepository<Course>().CreateAsync(course);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Khóa học được tạo thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo khóa học.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, CourseRequest course)
		{
			if (course == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu khóa học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var existingCourse = await courseRepository.GetByIdAsync(id);

				if (existingCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy khóa học.");

				existingCourse.Title = course.Title;
				existingCourse.Price = course.Price;
				existingCourse.DifficultyLevel = course.DifficultyLevel;
				existingCourse.UpdatedAt = DateTime.UtcNow;

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();
				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Khóa học đã được cập nhật.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật khóa học.");
			}
		}

		public async Task<BaseResponse<bool>> DeleteCourseAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var existingCourse = await courseRepository.GetByIdAsync(id);

				if (existingCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy khóa học.");

				await courseRepository.DeleteAsync(existingCourse);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Khóa học đã được xóa.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa khóa học.");
			}
		}
	}
}
