using System.Linq.Expressions;
using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.reponse;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Core.Utils;
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

        public async Task<BaseResponse<CourseResponse>> GetCourseByIdAsync(Guid id)
        {
            try
            {
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var topicRepository = _unitOfWork.GetRepository<Topic>();
                var lessonRepository = _unitOfWork.GetCustomRepository<ILessonRepository>();
                var course = await courseRepository.GetByIdAsync(id, q => q.Include(c => c.Topic));
                if (course == null)
                    return new BaseResponse<CourseResponse>(
                        StatusCodeHelper.BadRequest,
                        "NOT_FOUND",
                        null,
                        "Khóa học không tồn tại."
                        
                    );
                var courseResponse = _mapper.Map<CourseResponse>(course);
                courseResponse.TopicName = course.Topic.Name;
                // Lấy danh sách lesson
                var lessons = await lessonRepository.GetLessonsByCourseId(id, 1, int.MaxValue);
                courseResponse.Lessons = _mapper.Map<IEnumerable<LessonResponse>>(lessons.Items);
                return new BaseResponse<CourseResponse>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    courseResponse,
                    "Lấy khóa học và danh sách bài học thành công."
                );
            }
            catch (Exception)
            {
                return new BaseResponse<CourseResponse>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    null,
                    "Lỗi hệ thống khi lấy khóa học."
                );
            }
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
		public async Task<BaseResponse<bool>> PurchaseCourseAsync(Guid courseId, string userId)
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
                var existingPurchase = await courseHistoryRepository.Entities.FirstOrDefaultAsync(
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
        //lấy course theo user, get all course
        public async Task<BaseResponse<IEnumerable<UserCourseResponse>>> GetUserCoursesWithProgressAsync(string userId)
        {
            try
            {
                var userCourseRepository = _unitOfWork.GetRepository<UserCourse>();
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var lessonRepository = _unitOfWork.GetRepository<Lesson>();

                // 1. Lấy danh sách các UserCourse của người dùng
                var userCourses = await userCourseRepository.Entities
                    .Where(uc => uc.UserId == userId)
                    .Include(uc => uc.Course)
                    .ToListAsync();

                if (userCourses == null || !userCourses.Any())
                {
                    return new BaseResponse<IEnumerable<UserCourseResponse>>(
                        StatusCodeHelper.OK, // Hoặc NotFound nếu bạn muốn
                        "NO_COURSES_FOUND",
                        new List<UserCourseResponse>(),
                        "Người dùng chưa đăng ký khóa học nào."
                    );
                }

                var userCourseResponses = new List<UserCourseResponse>();

                // 2. Lặp qua từng UserCourse và tính toán tiến độ
                foreach (var userCourse in userCourses)
                {
                    var course = userCourse.Course;
                    var lessonsInCourse = await lessonRepository.Entities
                        .Where(l => l.CourseID == course.CourseID)
                        .ToListAsync();

                    int totalLessons = lessonsInCourse.Count;
                    int completedLessons = 0;

                    if (totalLessons > 0)
                    {
                        // **Use this approach**
                        var lessonIds = lessonsInCourse.Select(l => l.LessonID).ToList();
                        completedLessons = await _unitOfWork.GetRepository<UserLesson>().Entities
                            .CountAsync(ul => ul.UserID == userId &&
                                             ul.LessonID != null &&
                                             lessonIds.Contains(ul.LessonID) &&
                                             ul.Progress == 100);
                    }

                    int progressPercentage = totalLessons > 0 ? (int)((double)completedLessons / totalLessons * 100) : 0;

                    var userCourseResponse = new UserCourseResponse
                    {
                        CourseID = course.CourseID,
                        CourseTitle = course.Title,
                        ProgressPercentage = progressPercentage,
                        TotalLessons = totalLessons,
                        CompletedLessons = completedLessons
                        // Thêm các thuộc tính khác bạn muốn trả về (ví dụ: CourseDescription, CourseImageUrl)
                    };

                    userCourseResponses.Add(userCourseResponse);
                }

                // 3. Trả về kết quả
                return new BaseResponse<IEnumerable<UserCourseResponse>>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    userCourseResponses,
                    "Lấy danh sách khóa học của người dùng thành công."
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách khóa học của người dùng. UserId: {UserId}", userId);
                return new BaseResponse<IEnumerable<UserCourseResponse>>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    null,
                    "Lỗi hệ thống khi lấy danh sách khóa học của người dùng."
                );
            }
        }
        public async Task<BaseResponse<CourseWithCompletionStatusResponse>> GetCourseWithCompletionStatusAsync(Guid courseId, string userId)
        {
            try
            {
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var lessonRepository = _unitOfWork.GetRepository<Lesson>();
                var userLessonRepository = _unitOfWork.GetRepository<UserLesson>();

                // 1. Lấy thông tin khóa học
                var course = await courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    return new BaseResponse<CourseWithCompletionStatusResponse>(
                        StatusCodeHelper.BadRequest,
                        "COURSE_NOT_FOUND",
                        null,
                        "Khóa học không tồn tại."
                    );
                }

                // 2. Lấy tổng số bài học trong khóa học
                var totalLessons = await lessonRepository.Entities
                    .CountAsync(l => l.CourseID == courseId);

                if (totalLessons == 0)
                {
                    // Trả về thông tin khóa học cơ bản nếu không có bài học
                    return new BaseResponse<CourseWithCompletionStatusResponse>(
                        StatusCodeHelper.OK,
                        "SUCCESS",
                        new CourseWithCompletionStatusResponse { CourseID = courseId, CourseTitle = course.Title },
                        "Khóa học không có bài học nào."
                    );
                }

                // 3. Lấy số bài học mà người dùng đã hoàn thành
                var lessonsInCourse = await lessonRepository.Entities
    .Where(l => l.CourseID == course.CourseID)
    .ToListAsync();
                var completedLessons = await _unitOfWork.GetRepository<UserLesson>().Entities
                        .Join(
                            lessonsInCourse,
                            ul => ul.LessonID,
                            l => l.LessonID,
                            (ul, l) => new { UserLesson = ul, Lesson = l } // Project into an anonymous type
                        )
                        .CountAsync(joined => joined.UserLesson.UserID == userId &&
                     joined.UserLesson.LessonID != null &&
                     joined.UserLesson.Progress == 100);
                // 4. Tính toán trạng thái hoàn thành
                bool isCompleted = (completedLessons == totalLessons) && (totalLessons > 0);
                int progressPercentage = totalLessons > 0 ? (int)((double)completedLessons / totalLessons * 100) : 0;

                // 5. Tạo DTO phản hồi
                var courseWithStatus = new CourseWithCompletionStatusResponse
                {
                    CourseID = course.CourseID,
                    CourseTitle = course.Title,
                    IsCompleted = isCompleted,
                    TotalLessons = totalLessons,
                    CompletedLessonsCount = completedLessons,
                    ProgressPercentage = progressPercentage
                    // Thêm các thuộc tính khác bạn muốn trả về (ví dụ: CourseDescription, CourseImageUrl)
                };

                return new BaseResponse<CourseWithCompletionStatusResponse>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    courseWithStatus,
                    "Lấy thông tin khóa học thành công."
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin khóa học. CourseId: {CourseId}, UserId: {UserId}", courseId, userId);
                return new BaseResponse<CourseWithCompletionStatusResponse>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    null,
                    "Lỗi hệ thống."
                );
            }
        }

		public async Task<BaseResponse<bool>> DeleteCourseAsync(Guid id)
		{
			if (id == Guid.Empty)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_ID", "ID không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var courseRepository = _unitOfWork.GetRepository<Course>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				// Kiểm tra course có tồn tại không
				var existingCourse = await courseRepository.GetByIdAsync(id);
				if (existingCourse == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", "Không tìm thấy khóa học.");

				// Kiểm tra sự tồn tại của Lesson trong Course
				var hasLessons = await lessonRepository.Entities.AnyAsync(l => l.CourseID == id);

				// Nếu tồn tại Lesson, từ chối xóa Course
				if (hasLessons)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "RELATED_ITEMS_EXIST", "Không thể xóa khóa học vì vẫn còn bài học liên quan.");

				// Thực hiện xóa Course
				await courseRepository.DeleteAsync(existingCourse.CourseID);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Khóa học đã được xóa thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, $"Lỗi hệ thống khi xóa khóa học: {ex.Message}");
			}
		}

		public async Task<BaseResponse<IEnumerable<CourseResponse>>> SearchCoursesByTitleAsync(string title, int pageIndex, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new BaseResponse<IEnumerable<CourseResponse>>(
                    StatusCodeHelper.BadRequest,
                    "INVALID_SEARCH_TERM",
                    new List<CourseResponse>(),
                    "Từ khóa tìm kiếm không hợp lệ."
                );
            }

            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                var courseRepository = _unitOfWork.GetRepository<Course>();
                var topicRepository = _unitOfWork.GetRepository<Topic>();
                var lessonRepository = _unitOfWork.GetCustomRepository<ILessonRepository>();

                // 1. Tìm kiếm các khóa học theo tiêu đề (sử dụng Contains để tìm kiếm gần đúng)
                var query = courseRepository.Entities.Where(c => c.Title.Contains(title));

                // 2. Áp dụng phân trang
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
                    "Tìm kiếm khóa học thành công."
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tìm kiếm khóa học theo tiêu đề. Title: {Title}", title);
                return new BaseResponse<IEnumerable<CourseResponse>>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    new List<CourseResponse>(),
                    "Lỗi hệ thống khi tìm kiếm khóa học."
                );
            }
        }
    }
}
