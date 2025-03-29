using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;

namespace LearnEase.Service.Services
{
	public class FlashcardService : IFlashcardService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public FlashcardService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<IEnumerable<FlashcardResponse>>> GetFlashcardsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				var query = flashcardRepository.Entities;
				var flashcards = await flashcardRepository.GetPaggingAsync(query, pageIndex, pageSize);

				var responseList = new List<FlashcardResponse>();
				foreach (var fc in flashcards.Items)
				{
					var lesson = await lessonRepository.GetByIdAsync(fc.LessonID);

					responseList.Add(new FlashcardResponse
					{
						FlashcardID = fc.FlashcardID,
						LessonID = fc.LessonID,
						LessonType = (LessonTypeEnum)lesson.LessonType,
						Front = fc.Front,
						Back = fc.Back,
						PronunciationAudioURL = fc.PronunciationAudioURL,
						CreatedAt = fc.CreatedAt
					});
				}

				return new BaseResponse<IEnumerable<FlashcardResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					responseList,
					"Lấy danh sách flashcard thành công."
				);
			}
			catch (Exception)
			{
				return new BaseResponse<IEnumerable<FlashcardResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<FlashcardResponse>(),
					"Lỗi hệ thống khi lấy danh sách flashcard."
				);
			}
		}

		public async Task<BaseResponse<FlashcardResponse>> GetFlashcardByIdAsync(Guid id)
		{
			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				var flashcard = await flashcardRepository.GetByIdAsync(id);
				if (flashcard == null)
					return new BaseResponse<FlashcardResponse>(StatusCodeHelper.NotFound, "NOT_FOUND", null, "Flashcard không tồn tại.");

				var lesson = await lessonRepository.GetByIdAsync(flashcard.LessonID);

				var response = new FlashcardResponse
				{
					FlashcardID = flashcard.FlashcardID,
					LessonID = flashcard.LessonID,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					Front = flashcard.Front,
					Back = flashcard.Back,
					PronunciationAudioURL = flashcard.PronunciationAudioURL,
					CreatedAt = flashcard.CreatedAt
				};

				return new BaseResponse<FlashcardResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy flashcard thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<FlashcardResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy flashcard.");
			}
		}

		public async Task<BaseResponse<bool>> CreateFlashcardAsync(FlashcardRequest flashcardRequest)
		{
			if (flashcardRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu flashcard không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var lesson = await lessonRepository.GetByIdAsync(flashcardRequest.LessonID);

				if (lesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "LESSON_NOT_FOUND", false, "Lesson không tồn tại.");

				var flashcard = _mapper.Map<Flashcard>(flashcardRequest);
				flashcard.CreatedAt = DateTime.UtcNow;

				lesson.LessonType = LessonTypeEnum.Flashcard;
				await _unitOfWork.GetRepository<Flashcard>().CreateAsync(flashcard);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard được tạo thành công.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo flashcard.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateFlashcardAsync(Guid id, FlashcardRequest flashcardRequest)
		{
			if (flashcardRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu flashcard không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
				var existingFlashcard = await flashcardRepository.GetByIdAsync(id);

				if (existingFlashcard == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy flashcard.");

				_mapper.Map(flashcardRequest, existingFlashcard);
				await flashcardRepository.UpdateAsync(existingFlashcard);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard đã được cập nhật.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
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
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy flashcard.");

				await flashcardRepository.DeleteAsync(existingFlashcard.FlashcardID);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Flashcard đã được xóa.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa flashcard.");
			}
		}
	}
}
