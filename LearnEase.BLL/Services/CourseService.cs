using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
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

		public async Task<BaseResponse<IEnumerable<CourseResponse>>> GetCoursesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var topicRepository = _unitOfWork.GetRepository<Topic>();

				// Phân trang cho course
				var query = courseRepository.Entities;
				var paginatedResult = await courseRepository.GetPaggingAsync(query, pageIndex, pageSize);
				List<CourseResponse> course = new List<CourseResponse>();

                foreach (var item in paginatedResult.Items)
                {
					var courseResponse = _mapper.Map<CourseResponse>(item);
					// Lấy Topic name gán vào course response
					var topic = await topicRepository.GetByIdAsync(item.TopicID);
					courseResponse.TopicName = topic.Name;
					// Thêm vào DB
					course.Add(courseResponse);
				}

				return new BaseResponse<IEnumerable<CourseResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					course,
					"Lấy danh sách khóa học thành công."
				);
			}
			catch (Exception)
			{
				return new BaseResponse<IEnumerable<CourseResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<CourseResponse>(),
					"Lỗi hệ thống khi lấy danh sách khóa học."
				);
			}
		}

		public async Task<BaseResponse<CourseResponse>> GetCourseByIdAsync(Guid id)
		{
			try
			{
				var course = await _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
				CourseResponse courseDTO = _mapper.Map<CourseResponse>(course);

				if (course == null)
					return new BaseResponse<CourseResponse>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Khóa học không tồn tại.");

				return new BaseResponse<CourseResponse>(StatusCodeHelper.OK, "SUCCESS", courseDTO, "Lấy khóa học thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<CourseResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy khóa học.");
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

		public async Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, CourseRequest courseRequest)
		{
			if (courseRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu khóa học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var existingCourse = await courseRepository.GetByIdAsync(id);

				if (existingCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy khóa học.");

				// ✅ Sử dụng AutoMapper để ánh xạ dữ liệu
				_mapper.Map(courseRequest, existingCourse);
				existingCourse.UpdatedAt = DateTime.UtcNow;

				await courseRepository.UpdateAsync(existingCourse);
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
