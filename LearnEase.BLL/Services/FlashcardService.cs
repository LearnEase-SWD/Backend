using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
	public class FlashcardService : IFlashcardService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<FlashcardService> _logger;

		public FlashcardService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FlashcardService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<BaseResponse<IEnumerable<Flashcard>>> GetFlashcardsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var query = flashcardRepository.Entities;
				var flashcards = await flashcardRepository.GetPaggingAsync(query, pageIndex, pageSize);

				return new BaseResponse<IEnumerable<Flashcard>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					flashcards.Items,
					"Lấy danh sách flashcard thành công."
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy danh sách flashcard: {ex.Message}");
				return new BaseResponse<IEnumerable<Flashcard>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<Flashcard>(),
					"Lỗi hệ thống khi lấy danh sách flashcard."
				);
			}
		}

		public async Task<BaseResponse<Flashcard>> GetFlashcardByIdAsync(Guid id)
		{
			try
			{
				var flashcard = await _unitOfWork.GetRepository<Flashcard>().GetByIdAsync(id);
				if (flashcard == null)
					return new BaseResponse<Flashcard>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Flashcard không tồn tại.");

				return new BaseResponse<Flashcard>(StatusCodeHelper.OK, "SUCCESS", flashcard, "Lấy flashcard thành công.");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy flashcard với ID {id}: {ex.Message}");
				return new BaseResponse<Flashcard>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy flashcard.");
			}
		}

		public async Task<BaseResponse<bool>> CreateFlashcardAsync(FlashcardRequest flashcardRequest)
		{
			if (flashcardRequest == null || string.IsNullOrWhiteSpace(flashcardRequest.Front) || string.IsNullOrWhiteSpace(flashcardRequest.Back))
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu flashcard không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var flashcard = _mapper.Map<Flashcard>(flashcardRequest);
				flashcard.CreatedAt = DateTime.UtcNow;
				
				await _unitOfWork.GetRepository<Flashcard>().CreateAsync(flashcard);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard được tạo thành công.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi tạo flashcard: {ex.Message}");
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo flashcard.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateFlashcardAsync(Guid id, FlashcardRequest flashcardRequest)
		{
			if (flashcardRequest == null || string.IsNullOrWhiteSpace(flashcardRequest.Front) || string.IsNullOrWhiteSpace(flashcardRequest.Back))
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu flashcard không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var existingFlashcard = await flashcardRepository.GetByIdAsync(id);

				if (existingFlashcard == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy flashcard.");

				existingFlashcard.Front = flashcardRequest.Front;
				existingFlashcard.Back = flashcardRequest.Back;
				existingFlashcard.PronunciationAudioURL = flashcardRequest.PronunciationAudioURL;
				existingFlashcard.LessonID = flashcardRequest.LessonID;
				existingFlashcard.CreatedAt = DateTime.UtcNow;

				await flashcardRepository.UpdateAsync(existingFlashcard);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard đã được cập nhật.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi cập nhật flashcard {id}: {ex.Message}");
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật flashcard.");
			}
		}

		public async Task<BaseResponse<bool>> DeleteFlashcardAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var existingFlashcard = await flashcardRepository.GetByIdAsync(id);

				if (existingFlashcard == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy flashcard.");

				await flashcardRepository.DeleteAsync(existingFlashcard);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard đã được xóa.");
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi xóa flashcard {id}: {ex.Message}");
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa flashcard.");
			}
		}
	}
}
