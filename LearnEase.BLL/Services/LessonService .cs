using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

		public async Task<BaseResponse<IEnumerable<LessonResponse>>> GetLessonsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();

				// Lấy danh sách bài học với phân trang
				var paginatedLessons = await lessonRepository.GetPaggingAsync(lessonRepository.Entities, pageIndex, pageSize);

				// Lấy danh sách ID bài học
				var lessonIds = paginatedLessons.Items.Select(l => l.LessonID).ToList();

				// Lấy dữ liệu liên quan
				var videoLessons = await videoLessonRepository.Entities
					.Where(v => lessonIds.Contains(v.LessonID))
					.ToListAsync();

				var theoryLessons = await theoryLessonRepository.Entities
					.Where(t => lessonIds.Contains(t.LessonID))
					.ToListAsync();

				var exercises = await exerciseRepository.Entities
					.Where(e => lessonIds.Contains(e.LessonID))
					.ToListAsync();

				var flashcards = await flashcardRepository.Entities
					.Where(f => lessonIds.Contains(f.LessonID))
					.ToListAsync();

				// Ánh xạ dữ liệu sang LessonResponse
				var lessonResponses = paginatedLessons.Items.Select(lesson => new LessonResponse
				{
					LessonID = lesson.LessonID,
					CourseID = lesson.CourseID,
					Index = lesson.Index,
					Title = lesson.Title,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					CreatedAt = lesson.CreatedAt,

					VideoLessons = _mapper.Map<IEnumerable<VideoLessonResponse>>(videoLessons.Where(v => v.LessonID == lesson.LessonID)),
					TheoryLessons = _mapper.Map<IEnumerable<TheoryLessonResponse>>(theoryLessons.Where(t => t.LessonID == lesson.LessonID)),
					Exercises = _mapper.Map<IEnumerable<ExerciseResponse>>(exercises.Where(e => e.LessonID == lesson.LessonID)),
					Flashcards = _mapper.Map<IEnumerable<FlashcardResponse>>(flashcards.Where(f => f.LessonID == lesson.LessonID))
				});

				return new BaseResponse<IEnumerable<LessonResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					lessonResponses,
					"Lấy danh sách bài học thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<LessonResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					$"Lỗi hệ thống khi lấy danh sách bài học: {ex.Message}"
				);
			}
		}

		public async Task<BaseResponse<IEnumerable<LessonResponse>>> GetLessonsByCourseIdAsync(Guid courseId, int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();

				// Lấy danh sách bài học theo CourseID
				var paginatedLessons = await lessonRepository.GetPaggingAsync(
					lessonRepository.Entities.Where(lesson => lesson.CourseID == courseId),
					pageIndex,
					pageSize
				);

				// Lấy danh sách ID bài học
				var lessonIds = paginatedLessons.Items.Select(l => l.LessonID).ToList();

				// Lấy dữ liệu liên quan
				var videoLessons = await videoLessonRepository.Entities
					.Where(v => lessonIds.Contains(v.LessonID))
					.ToListAsync();

				var theoryLessons = await theoryLessonRepository.Entities
					.Where(t => lessonIds.Contains(t.LessonID))
					.ToListAsync();

				var exercises = await exerciseRepository.Entities
					.Where(e => lessonIds.Contains(e.LessonID))
					.ToListAsync();

				var flashcards = await flashcardRepository.Entities
					.Where(f => lessonIds.Contains(f.LessonID))
					.ToListAsync();

				// Ánh xạ dữ liệu sang LessonResponse
				var lessonResponses = paginatedLessons.Items.Select(lesson => new LessonResponse
				{
					LessonID = lesson.LessonID,
					CourseID = lesson.CourseID,
					Index = lesson.Index,
					Title = lesson.Title,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					CreatedAt = lesson.CreatedAt,

					VideoLessons = _mapper.Map<IEnumerable<VideoLessonResponse>>(videoLessons.Where(v => v.LessonID == lesson.LessonID)),
					TheoryLessons = _mapper.Map<IEnumerable<TheoryLessonResponse>>(theoryLessons.Where(t => t.LessonID == lesson.LessonID)),
					Exercises = _mapper.Map<IEnumerable<ExerciseResponse>>(exercises.Where(e => e.LessonID == lesson.LessonID)),
					Flashcards = _mapper.Map<IEnumerable<FlashcardResponse>>(flashcards.Where(f => f.LessonID == lesson.LessonID))
				});

				return new BaseResponse<IEnumerable<LessonResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					lessonResponses,
					"Lấy danh sách bài học theo khóa học thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<IEnumerable<LessonResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					$"Lỗi hệ thống: {ex.Message}"
				);
			}
		}

		public async Task<BaseResponse<LessonResponse>> GetLessonByIdAsync(Guid id)
		{
			if (id == Guid.Empty)
				return new BaseResponse<LessonResponse>(
					StatusCodeHelper.BadRequest,
					"INVALID_ID",
					null,
					"ID không hợp lệ."
				);

			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();

				// Lấy bài học theo ID
				var lesson = await lessonRepository.GetByIdAsync(id);
				if (lesson == null)
					return new BaseResponse<LessonResponse>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						null,
						"Bài học không tồn tại."
					);

				// Lấy dữ liệu liên quan
				var videoLessons = await videoLessonRepository.Entities
					.Where(v => v.LessonID == id)
					.ToListAsync();

				var theoryLessons = await theoryLessonRepository.Entities
					.Where(t => t.LessonID == id)
					.ToListAsync();

				var exercises = await exerciseRepository.Entities
					.Where(e => e.LessonID == id)
					.ToListAsync();

				var flashcards = await flashcardRepository.Entities
					.Where(f => f.LessonID == id)
					.ToListAsync();

				// Ánh xạ sang LessonResponse
				var lessonResponse = new LessonResponse
				{
					LessonID = lesson.LessonID,
					CourseID = lesson.CourseID,
					Index = lesson.Index,
					Title = lesson.Title,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					CreatedAt = lesson.CreatedAt,

					VideoLessons = _mapper.Map<IEnumerable<VideoLessonResponse>>(videoLessons),
					TheoryLessons = _mapper.Map<IEnumerable<TheoryLessonResponse>>(theoryLessons),
					Exercises = _mapper.Map<IEnumerable<ExerciseResponse>>(exercises),
					Flashcards = _mapper.Map<IEnumerable<FlashcardResponse>>(flashcards)
				};

				return new BaseResponse<LessonResponse>(
					StatusCodeHelper.OK,
					"SUCCESS",
					lessonResponse,
					"Lấy bài học thành công."
				);
			}
			catch (Exception ex)
			{
				return new BaseResponse<LessonResponse>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					$"Lỗi hệ thống khi lấy bài học: {ex.Message}"
				);
			}
		}

		public async Task<BaseResponse<bool>> CreateLessonAsync(LessonCreateRequest lessonRequest)
        {
            // Kiểm tra null
            if (lessonRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", "Dữ liệu bài học không hợp lệ.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lesson = _mapper.Map<Lesson>(lessonRequest);
                lesson.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.GetRepository<Lesson>().CreateAsync(lesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học được tạo thành công.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài học.");
            }
        }

        public async Task<BaseResponse<bool>> UpdateLessonAsync(Guid id, LessonCreateRequest lessonRequest)
        {
            if (id == Guid.Empty || lessonRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", "ID hoặc dữ liệu cập nhật không hợp lệ.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lessonRepository = _unitOfWork.GetRepository<Lesson>();
                var existingLesson = await lessonRepository.GetByIdAsync(id);

                if (existingLesson == null)
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", "Không tìm thấy bài học.");

                _mapper.Map(lessonRequest, existingLesson);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được cập nhật.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật bài học.");
            }
        }

		public async Task<BaseResponse<bool>> DeleteLessonAsync(Guid id)
		{
			if (id == Guid.Empty)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_ID", "ID không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();

				// Kiểm tra lesson có tồn tại không
				var existingLesson = await lessonRepository.GetByIdAsync(id);
				if (existingLesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", "Không tìm thấy bài học.");

				// Kiểm tra sự tồn tại của VideoLesson, TheoryLesson, Exercise, Flashcard
				var hasVideoLessons = await videoLessonRepository.Entities.AnyAsync(v => v.LessonID == id);
				var hasTheoryLessons = await theoryLessonRepository.Entities.AnyAsync(t => t.LessonID == id);
				var hasExercises = await exerciseRepository.Entities.AnyAsync(e => e.LessonID == id);
				var hasFlashcards = await flashcardRepository.Entities.AnyAsync(f => f.LessonID == id);

				// Nếu bất kỳ thành phần nào tồn tại, từ chối xóa
				if (hasVideoLessons || hasTheoryLessons || hasExercises || hasFlashcards)
				{
					var errorMessage = "Không thể xóa bài học vì vẫn còn các thành phần liên quan:";
					if (hasVideoLessons) errorMessage += " VideoLesson";
					if (hasTheoryLessons) errorMessage += " TheoryLesson";
					if (hasExercises) errorMessage += " Exercise";
					if (hasFlashcards) errorMessage += " Flashcard";

					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "RELATED_ITEMS_EXIST", errorMessage);
				}

				await lessonRepository.DeleteAsync(existingLesson.LessonID);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được xóa.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, $"Lỗi hệ thống khi xóa bài học: {ex.Message}");
			}
		}
	}
}
