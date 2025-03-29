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

		// 1. Tạo VideoLesson
		public async Task<BaseResponse<bool>> CreateVideoLessonAsync(VideoLessonCreateRequest videoRequest)
		{
			if (videoRequest == null)
				return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();

				var lesson = await lessonRepository.GetByIdAsync(videoRequest.LessonID);
				if (lesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "LESSON_NOT_FOUND", false, "Lesson không tồn tại.");

				lesson.LessonType = LessonTypeEnum.Video;
				await lessonRepository.UpdateAsync(lesson);

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

		// 2. Lấy danh sách VideoLesson
		public async Task<BaseResponse<IEnumerable<VideoLessonResponse>>> GetVideoLessonsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				// Lấy danh sách VideoLesson với phân trang
				var query = videoLessonRepository.Entities;
				var videoLessons = await videoLessonRepository.GetPaggingAsync(query, pageIndex, pageSize);

				// Duyệt từng phần tử và lấy Lesson tương ứng
				var responseList = new List<VideoLessonResponse>();
				foreach (var vl in videoLessons.Items)
				{
					// Lấy từng lesson theo ID
					var lesson = await lessonRepository.GetByIdAsync(vl.LessonID);

					responseList.Add(new VideoLessonResponse
					{
						VideoID = vl.VideoID,
						LessonID = vl.LessonID,
						LessonType = (LessonTypeEnum)lesson.LessonType,
						Duration = vl.Duration,
						VideoURL = vl.VideoURL,
						CreatedAt = vl.CreatedAt
					});
				}

				return new BaseResponse<IEnumerable<VideoLessonResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					responseList,
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

		// 3. Lấy VideoLesson theo ID
		public async Task<BaseResponse<VideoLessonResponse>> GetVideoLessonByIdAsync(Guid id)
		{
			try
			{
				var videoLesson = await _unitOfWork.GetRepository<VideoLesson>().GetByIdAsync(id);
				if (videoLesson == null)
					return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.NotFound, "NOT_FOUND", null, "Bài học video không tồn tại.");

				var response = _mapper.Map<VideoLessonResponse>(videoLesson);
				return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy bài học video thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<VideoLessonResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài học video.");
			}
		}

		// 4. Cập nhật VideoLesson
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
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy bài học video.");

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

		// 5. Xóa VideoLesson
		public async Task<BaseResponse<bool>> DeleteVideoLessonAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
				var existingLesson = await videoLessonRepository.GetByIdAsync(id);

				if (existingLesson == null)
					return new BaseResponse<bool>(StatusCodeHelper.NotFound, "NOT_FOUND", false, "Không tìm thấy bài học video.");

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

            public async Task<BaseResponse<bool>> MarkVideoLessonAsCompletedAsync(string userId, Guid videoLessonId)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userVideoLessonRepository = _unitOfWork.GetRepository<UserVideoLesson>();
                var videoLessonRepository = _unitOfWork.GetRepository<VideoLesson>();
                var userLessonRepository = _unitOfWork.GetRepository<UserLesson>();

                // Kiểm tra xem VideoLesson có tồn tại không
                var videoLesson = await videoLessonRepository.GetByIdAsync(videoLessonId);
                if (videoLesson == null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.NotFound, "VIDEO_LESSON_NOT_FOUND", false, "Bài học video không tồn tại.");
                }

                // Tìm UserLesson tương ứng
                var userLesson = await userLessonRepository.FirstOrDefaultAsync(ul => ul.UserID == userId && ul.LessonID == videoLesson.LessonID);
                if (userLesson == null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.NotFound, "USER_LESSON_NOT_FOUND", false, "UserLesson không tồn tại");
                }

                // Kiểm tra xem UserVideoLesson có tồn tại không
                var existingUserVideoLesson = await userVideoLessonRepository.FirstOrDefaultAsync(uvl => uvl.UserID == userId && uvl.VideoLessonID == videoLessonId);

                if (existingUserVideoLesson == null)
                {
                    // Nếu chưa tồn tại, tạo mới và đánh dấu là đã hoàn thành
                    var newUserVideoLesson = new UserVideoLesson
                    {
                        UserID = userId,
                        VideoLessonID = videoLessonId,
                        IsCompleted = true,
                        LastAccessedAt = DateTime.UtcNow
                    };

                    await userVideoLessonRepository.CreateAsync(newUserVideoLesson);
                }
                else
                {
                    // Nếu đã tồn tại, cập nhật trạng thái đã hoàn thành
                    existingUserVideoLesson.IsCompleted = true;
                    existingUserVideoLesson.LastAccessedAt = DateTime.UtcNow;
                    await userVideoLessonRepository.UpdateAsync(existingUserVideoLesson);
                }

                // Cập nhật UserLesson
                userLesson.IsVideoCompleted = true;
                await userLessonRepository.UpdateAsync(userLesson);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học video đã được đánh dấu là hoàn thành.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi đánh dấu bài học video.");
            }
        }
	}
}
