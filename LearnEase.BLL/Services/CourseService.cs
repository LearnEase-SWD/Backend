using System.Linq.Expressions;
using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
    public class CourseService : ICourseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
            _logger = logger;
        }

		public async Task<BaseResponse<IEnumerable<CourseResponse>>> GetCoursesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var topicRepository = _unitOfWork.GetRepository<Topic>();
				var lessonRepository = _unitOfWork.GetCustomRepository<ILessonRepository>();

				var query = courseRepository.Entities;
				var paginatedResult = await courseRepository.GetPaggingAsync(query, pageIndex, pageSize);

				List<CourseResponse> courses = new List<CourseResponse>();

				foreach (var item in paginatedResult.Items)
				{
					var courseResponse = _mapper.Map<CourseResponse>(item);

					// Lấy Topic Name
					var topic = await topicRepository.GetByIdAsync(item.TopicID);
					courseResponse.TopicName = topic.Name;

					// Lấy danh sách Lesson
					var lessons = await lessonRepository.GetLessonsByCourseId(item.CourseID, pageIndex, pageSize);
					courseResponse.Lessons = _mapper.Map<IEnumerable<LessonResponse>>(lessons.Items);

					courses.Add(courseResponse);
				}

				return new BaseResponse<IEnumerable<CourseResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					courses,
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

		//mua khóa học 
		/*public async Task<BaseResponse<bool>> PurchaseCourseAsync(Guid courseId, string userId)
        {
            // Kiểm tra userId hợp lệ trước khi mở transaction
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_USER", false, "User ID is invalid.");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var courseHistoryRepository = _unitOfWork.GetRepository<CourseHistory>();

                // 1. Get the course
                var course = await courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "COURSE_NOT_FOUND", false, "Course not found.");
                }

                // 2. Check if the user has already purchased the course
                var existingPurchase = await courseHistoryRepository.FirstOrDefaultAsync(
                    ch => ch.CourseID == courseId && ch.UserID == userId
                );

                if (existingPurchase != null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "ALREADY_PURCHASED", false, "You have already purchased this course.");
                }

                // 3. Create the CourseHistory record
                var courseHistory = new CourseHistory
                {
                    CourseID = courseId,
                    UserID = userId,
                    Price = course.Price,
                    PurchasedAt = DateTime.UtcNow
                };
                await courseHistoryRepository.CreateAsync(courseHistory);
                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "PURCHASE_SUCCESSFUL", true, "Course purchased successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                // Ghi log lỗi để debug
                _logger.LogError(ex, "Error purchasing course for user {UserId} and course {CourseId}", userId, courseId);

                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "PURCHASE_FAILED", false, "Failed to purchase course.");
            }
        }*/

		public async Task<BaseResponse<CourseResponse>> GetCourseByIdAsync(Guid id)
		{
			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var topicRepository = _unitOfWork.GetRepository<Topic>();
				var lessonRepository = _unitOfWork.GetCustomRepository<ILessonRepository>();

				var course = await courseRepository.GetByIdAsync(id, q => q.Include(c => c.Topic));

				if (course == null)
				{
					return new BaseResponse<CourseResponse>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						null,
						"Khóa học không tồn tại."
					);
				}

				var courseResponse = _mapper.Map<CourseResponse>(course);
				courseResponse.TopicName = course.Topic.Name;

				var lessons = await lessonRepository.GetLessonsByCourseId(id, 1, int.MaxValue); // Lấy tất cả
				courseResponse.Lessons = _mapper.Map<IEnumerable<LessonResponse>>(lessons.Items);

				return new BaseResponse<CourseResponse>(
					StatusCodeHelper.OK,
					"SUCCESS",
					courseResponse,
					"Lấy thông tin khóa học thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<CourseResponse>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					"Lỗi hệ thống khi lấy khóa học."
				);
			}
		}

		public async Task<BaseResponse<CourseResponse>> GetCourseByUserIdAsync(Guid id, string userId)
        {
            try
            {
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var topicRepository = _unitOfWork.GetRepository<Topic>();
                var lessonRepository = _unitOfWork.GetCustomRepository<ILessonRepository>();
                var userCourseRepository = _unitOfWork.GetRepository<UserCourse>();

                // 1. Lấy course và bao gồm topic
                var course = await courseRepository.GetByIdAsync(id, q => q.Include(c => c.Topic));
                if (course == null)
                {
                    return new BaseResponse<CourseResponse>(
                        StatusCodeHelper.BadRequest,
                        "NOT_FOUND",
                        null,
                        "Khóa học không tồn tại."
                    );
                }

                // 2. Kiểm tra quyền truy cập của người dùng (đã đăng ký khóa học chưa)
                //    Sử dụng FirstOrDefaultAsync (hoặc phương pháp tương đương trong repository của bạn)
                //    Nếu GetByIdAsync không hỗ trợ predicate, hãy sử dụng một cách khác (xem bên dưới)

                UserCourse? userCourse = null; // Initialize to null

                // If your repository has FirstOrDefaultAsync that supports predicate:
                // userCourse = await userCourseRepository.FirstOrDefaultAsync(uc => uc.CourseID == id && uc.UserId == userId);

                // If your repository DOES NOT have FirstOrDefaultAsync (as in your IGenericRepository):
                //  Use GetPaggingAsync as a workaround (efficiently retrieves at most one result)
                var userCourseQuery = _unitOfWork.GetRepository<UserCourse>().Entities
                    .Where(uc => uc.CourseID == id && uc.UserId == userId);

                var userCourseResult = await userCourseRepository.GetPaggingAsync(userCourseQuery, 1, 1);
                userCourse = userCourseResult.Items.FirstOrDefault(); // Safely get the first item (or null)


                if (userCourse == null)
                {
                    return new BaseResponse<CourseResponse>(
                        StatusCodeHelper.BadRequest,
                        "FORBIDDEN",
                        null,
                        "Bạn không có quyền truy cập khóa học này."
                    );
                }

                // 3. Ánh xạ Course và thêm TopicName
                var courseResponse = _mapper.Map<CourseResponse>(course);
                courseResponse.TopicName = course.Topic.Name;

                // 4. Lấy danh sách lesson (có thể phân trang hoặc lấy tất cả)
                var lessons = await lessonRepository.GetLessonsByCourseId(id, 1, int.MaxValue); // Lấy tất cả lessons (hoặc phân trang nếu cần)
                courseResponse.Lessons = _mapper.Map<IEnumerable<LessonResponse>>(lessons.Items);

                // 5. Trả về kết quả thành công
                return new BaseResponse<CourseResponse>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    courseResponse,
                    "Lấy khóa học và danh sách bài học thành công."
                );
            }
            catch (Exception ex)
            {
                // 6. Xử lý lỗi (log lỗi!)
               
                return new BaseResponse<CourseResponse>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    null,
                    "Lỗi hệ thống khi lấy khóa học."
                );
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
			catch (Exception)
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
