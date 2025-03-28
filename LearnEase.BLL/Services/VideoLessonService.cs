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
	public class VideoLessonService : IVideoLessonService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public VideoLessonService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BaseResponse<bool>> CreateVideoLessonAsync(VideoLessonCreateRequest videoRequest)
		{
			if (videoRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();

				// Kiểm tra LessonID đã tồn tại chưa
				var existingVideoLesson = await videoLessonRepository
					.GetByIdAsync(videoRequest.LessonID);

				if (existingVideoLesson == null)
				{
					await _unitOfWork.RollbackAsync();
					return new BaseResponse<bool>(
						StatusCodeHelper.BadRequest,
						"DUPLICATE_LESSON_ID",
						false,
						"LessonID đã tồn tại. Mỗi VideoLesson chỉ được phép có một LessonID duy nhất."
					);
				}

				var videoLesson = _mapper.Map<VideoLesson>(videoRequest);
				videoLesson.CreatedAt = DateTime.UtcNow;

				await videoLessonRepository.CreateAsync(videoLesson);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học video đã được tạo thành công.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài học video.");
			}
		}

		public async Task<BaseResponse<IEnumerable<VideoLessonResponse>>> GetVideoLessonsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var query = videoLessonRepository.Entities;
				var videoLessons = await videoLessonRepository.GetPaggingAsync(query, pageIndex, pageSize);

				var response = _mapper.Map<IEnumerable<VideoLessonResponse>>(videoLessons.Items);

				return new BaseResponse<IEnumerable<VideoLessonResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					response,
					"Lấy danh sách bài học video thành công."
				);
			}
			catch (Exception)
			{
				return new BaseResponse<IEnumerable<VideoLessonResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<VideoLessonResponse>(),
					"Lỗi hệ thống khi lấy danh sách bài học video."
				);
			}
		}

		public async Task<BaseResponse<VideoLessonResponse>> GetVideoLessonByIdAsync(Guid id)
		{
			try
			{
				var videoLesson = await _unitOfWork.GetRepository<VideoLesson>().GetByIdAsync(id);
				if (videoLesson == null)
					return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Bài học video không tồn tại.");

				var response = _mapper.Map<VideoLessonResponse>(videoLesson);
				return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy bài học video thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài học video.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateVideoLessonAsync(Guid id, VideoLessonCreateRequest request)
		{
			if (request == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var existingLesson = await videoLessonRepository.GetByIdAsync(id);

				if (existingLesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài học video.");

				_mapper.Map(request, existingLesson);

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();
				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học video đã được cập nhật.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật bài học video.");
			}
		}

		public async Task<BaseResponse<bool>> DeleteVideoLessonAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var existingLesson = await videoLessonRepository.GetByIdAsync(id);

				if (existingLesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài học video.");

				await videoLessonRepository.DeleteAsync(existingLesson);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học video đã được xóa.");
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài học video.");
			}
		}
	}
}
