using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Core.IServices;

namespace LearnEase.Service.Services
{
	public class ExerciseService : IExerciseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ExerciseService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<bool>> CreateExerciseAsync(ExerciseRequest exerciseRequest)
		{
			if (exerciseRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài tập không hợp lệ.");

			var lessonRepository = _unitOfWork.GetRepository<Lesson>();
			Lesson lesson = await lessonRepository.GetByIdAsync(exerciseRequest.LessonID);
			if (lesson == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "ID bài học không tồn tại.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var exercise = _mapper.Map<Exercise>(exerciseRequest);
				exercise.CreatedAt = DateTime.UtcNow;

				lesson.LessonType = LessonTypeEnum.Exercise;

				await _unitOfWork.GetRepository<Exercise>().CreateAsync(exercise);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được tạo thành công.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài tập.");
			}
		}

		public async Task<BaseResponse<IEnumerable<ExerciseResponse>>> GetExercisesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				var query = exerciseRepository.Entities;
				var exercises = await exerciseRepository.GetPaggingAsync(query, pageIndex, pageSize);

				var responseList = new List<ExerciseResponse>();
				foreach (var ex in exercises.Items)
				{
					var lesson = await lessonRepository.GetByIdAsync(ex.LessonID);

					responseList.Add(new ExerciseResponse
					{
						ExerciseID = ex.ExerciseID,
						LessonID = ex.LessonID,
						ExerciseType = ex.Type,
						CorrectAnswer = ex.CorrectAnswer,
						LessonType = (LessonTypeEnum)lesson.LessonType,
						Question = ex.Question,
						AnswerOptions = ex.AnswerOptions,
						CreatedAt = ex.CreatedAt
					});
				}

				return new BaseResponse<IEnumerable<ExerciseResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					responseList,
					"Lấy danh sách bài tập thành công."
				);
			}
			catch (Exception)
			{
				return new BaseResponse<IEnumerable<ExerciseResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<ExerciseResponse>(),
					"Lỗi hệ thống khi lấy danh sách bài tập."
				);
			}
		}

		public async Task<BaseResponse<ExerciseResponse>> GetExerciseByIdAsync(Guid id)
		{
			try
			{
				var exercise = await _unitOfWork.GetRepository<Exercise>().GetByIdAsync(id);
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				if (exercise == null)
					return new BaseResponse<ExerciseResponse>(StatusCodeHelper.NotFound, "NOT_FOUND", null, "Bài tập không tồn tại.");

				var lesson = await lessonRepository.GetByIdAsync(exercise.LessonID);

				var response = new ExerciseResponse
				{
					ExerciseID = exercise.ExerciseID,
					LessonID = exercise.LessonID,
					ExerciseType = exercise.Type,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					Question = exercise.Question,
					AnswerOptions = exercise.AnswerOptions,
					CorrectAnswer = exercise.CorrectAnswer,
					CreatedAt = exercise.CreatedAt
				};

				return new BaseResponse<ExerciseResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy bài tập thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<ExerciseResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài tập.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateExerciseAsync(Guid id, ExerciseRequest request)
		{
			if (request == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài tập không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var existingExercise = await exerciseRepository.GetByIdAsync(id);

				if (existingExercise == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy bài tập.");

				_mapper.Map(request, existingExercise);

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();
				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được cập nhật.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật bài tập.");
			}
		}

		public async Task<BaseResponse<bool>> DeleteExerciseAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var existingExercise = await exerciseRepository.GetByIdAsync(id);

				if (existingExercise == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy bài tập.");

				await exerciseRepository.DeleteAsync(existingExercise.ExerciseID);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được xóa.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài tập.");
			}
		}
		public async Task<BaseResponse<bool>> MarkExerciseAsCompletedAsync(string userId, Guid exerciseId)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var userExerciseRepository = _unitOfWork.GetRepository<UserExercise>();
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var userLessonRepository = _unitOfWork.GetRepository<UserLesson>();

				// Kiểm tra xem Exercise có tồn tại không
				var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
				if (exercise == null)
				{
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "EXERCISE_NOT_FOUND", false, "Bài tập không tồn tại.");
				}

				// Tìm UserLesson tương ứng
				var userLesson = await userLessonRepository.FirstOrDefaultAsync(ul => ul.UserID == userId && ul.LessonID == exercise.LessonID);
				if (userLesson == null)
				{
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "USER_LESSON_NOT_FOUND", false, "UserLesson không tồn tại");
				}

				// Tạo mới UserExercise hoặc cập nhật
				var existingUserExercise = await userExerciseRepository.FirstOrDefaultAsync(ue => ue.UserID == userId && ue.ExerciseID == exerciseId);

				if (existingUserExercise == null)
				{
					// Tạo mới UserExercise nếu chưa tồn tại.
					var newUserExercise = new UserExercise()
					{
						UserID = userId,
						ExerciseID = exerciseId,
						Progress = "Completed"
					};
					await userExerciseRepository.CreateAsync(newUserExercise);
				}
				else
				{
					existingUserExercise.Progress = "Completed";
					await userExerciseRepository.UpdateAsync(existingUserExercise);
				}

				// Cập nhật UserLesson
				userLesson.IsExerciseCompleted = true;
				await userLessonRepository.UpdateAsync(userLesson);

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được đánh dấu là hoàn thành.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi đánh dấu bài tập.");
			}
		}
	}
}
