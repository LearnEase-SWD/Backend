using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
	public class ExerciseService : IExerciseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<ExerciseService> _logger;

		public ExerciseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ExerciseService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<BaseResponse<IEnumerable<Exercise>>> GetExercisesAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var query = exerciseRepository.Entities;
				var paginatedResult = await exerciseRepository.GetPagging(query, pageIndex, pageSize);

				return new BaseResponse<IEnumerable<Exercise>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					paginatedResult.Items,
					"Lấy danh sách bài tập thành công."
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy danh sách bài tập: {ex.Message}");
				return new BaseResponse<IEnumerable<Exercise>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<Exercise>(),
					"Lỗi hệ thống khi lấy danh sách bài tập."
				);
			}
		}

		public async Task<BaseResponse<Exercise>> GetExerciseByIdAsync(Guid id)
		{
			try
			{
				var exercise = await _unitOfWork.GetRepository<Exercise>().GetByIdAsync(id);
				if (exercise == null)
					return new BaseResponse<Exercise>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Bài tập không tồn tại.");

				return new BaseResponse<Exercise>(StatusCodeHelper.OK, "SUCCESS", exercise, "Lấy bài tập thành công.");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy bài tập với ID {id}: {ex.Message}");
				return new BaseResponse<Exercise>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài tập.");
			}
		}

		public async Task<BaseResponse<bool>> CreateExerciseAsync(ExerciseRequest exerciseRequest)
		{
			if (exerciseRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài tập không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var exercise = _mapper.Map<Exercise>(exerciseRequest);
				exercise.CreatedAt = DateTime.Now;
				await _unitOfWork.GetRepository<Exercise>().CreateAsync(exercise);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập được tạo thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi tạo bài tập: {ex.Message}");
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài tập.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateExerciseAsync(Guid id, ExerciseRequest exercise)
		{
			if (exercise == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài tập không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
				var existingExercise = await exerciseRepository.GetByIdAsync(id);

				if (existingExercise == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài tập.");

				existingExercise.Type = exercise.Type;
				existingExercise.Question = exercise.Question;
				existingExercise.CorrectAnswer = exercise.CorrectAnswer;
				existingExercise.LessonID = exercise.LessonID;
				existingExercise.CreatedAt = DateTime.UtcNow;

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();
				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được cập nhật.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi cập nhật bài tập {id}: {ex.Message}");
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
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài tập.");

				await exerciseRepository.DeleteAsync(existingExercise);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài tập đã được xóa.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi xóa bài tập {id}: {ex.Message}");
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài tập.");
			}
		}
	}
}
