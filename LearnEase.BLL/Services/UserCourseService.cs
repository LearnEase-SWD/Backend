using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.IServices;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
	public class UserCourseService : IUserCourseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<UserCourseService> _logger;

		public UserCourseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserCourseService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<BaseResponse<IEnumerable<UserCourseResponse>>> GetUserCoursesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var repository = _unitOfWork.GetRepository<UserCourse>();
				var userCourses = await repository.GetPaggingAsync(repository.Entities
					.Include(uc => uc.User)
					.Include(uc => uc.Course), pageIndex, pageSize);

				var response = _mapper.Map<IEnumerable<UserCourseResponse>>(userCourses.Items);
				return new BaseResponse<IEnumerable<UserCourseResponse>>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy danh sách UserCourse thành công.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new BaseResponse<IEnumerable<UserCourseResponse>>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống.");
			}
		}

		public async Task<BaseResponse<UserCourseResponse>> GetUserCourseByIdAsync(Guid userCourseId)
		{
			try
			{
				var repository = _unitOfWork.GetRepository<UserCourse>();
				var userCourse = await repository.GetByIdAsync(userCourseId, q => q.Include(uc => uc.User).Include(uc => uc.Course));

				if (userCourse == null)
					return new BaseResponse<UserCourseResponse>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Không tìm thấy UserCourse.");

				var response = _mapper.Map<UserCourseResponse>(userCourse);
				return new BaseResponse<UserCourseResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy UserCourse thành công.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return new BaseResponse<UserCourseResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống.");
			}
		}

		// Lấy course theo user (user mua course nào thì hiện course đó)
		public async Task<BaseResponse<IEnumerable<CourseResponse>>> GetCoursesByUserAsync(string userId)
		{
			try
			{
				var userCourseRepository = _unitOfWork.GetRepository<UserCourse>();
				var courseRepository = _unitOfWork.GetRepository<Course>();

				var userCourses = await userCourseRepository.FindAllAsync(
					uc => uc.UserId == userId,
					query => query.Include(uc => uc.Course)
				);

				if (userCourses == null || !userCourses.Any())
				{
					return new BaseResponse<IEnumerable<CourseResponse>>(
						StatusCodeHelper.OK,
						"NOT_FOUND",
						null,
						"Người dùng chưa đăng ký bất kỳ khóa học nào."
					);
				}

				var courses = userCourses.Select(uc => uc.Course).ToList();
				var courseResponses = _mapper.Map<IEnumerable<CourseResponse>>(courses);

				return new BaseResponse<IEnumerable<CourseResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					courseResponses,
					"Lấy danh sách khóa học của người dùng thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<CourseResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					"Lỗi hệ thống khi lấy danh sách khóa học của người dùng."
				);
			}
		}

		public async Task<BaseResponse<bool>> CreateUserCourseAsync(UserCourseRequest userCourseRequest)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var userCourse = _mapper.Map<UserCourse>(userCourseRequest);
				userCourse.EnrolledAt = DateTime.UtcNow;

				await _unitOfWork.GetRepository<UserCourse>().CreateAsync(userCourse);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Tạo UserCourse thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError(ex.Message);
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi khi tạo UserCourse.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateUserCourseAsync(Guid userCourseId, UserCourseRequest userCourseRequest)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var repository = _unitOfWork.GetRepository<UserCourse>();
				var existingUserCourse = await repository.GetByIdAsync(userCourseId);

				if (existingUserCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy UserCourse.");

				_mapper.Map(userCourseRequest, existingUserCourse);
				await repository.UpdateAsync(existingUserCourse);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Cập nhật UserCourse thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError(ex.Message);
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi khi cập nhật UserCourse.");
			}
		}

		public async Task<BaseResponse<bool>> DeleteUserCourseAsync(Guid userCourseId)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var repository = _unitOfWork.GetRepository<UserCourse>();
				var existingUserCourse = await repository.GetByIdAsync(userCourseId);

				if (existingUserCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy UserCourse.");

				await repository.DeleteAsync(existingUserCourse);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Xóa UserCourse thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError(ex.Message);
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi khi xóa UserCourse.");
			}
		}

		public async Task<BaseResponse<UserCourseResponse>> GetUserCourseByIdCourseAsync(Guid CourseId, Guid userId)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var repository = _unitOfWork.GetRepository<UserCourse>();
				var existingUserCourse = await repository.FindOneAsync(u => u.CourseID == CourseId && u.UserId.Equals(userId));
				if (existingUserCourse == null)
				{
					return new BaseResponse<UserCourseResponse>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Không tìm thấy UserCourse.");
				}

				var response = _mapper.Map<UserCourseResponse>(existingUserCourse);
				return new BaseResponse<UserCourseResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy UserCourse thành công.");
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				return new BaseResponse<UserCourseResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống.");
			}
		}
	}
}
