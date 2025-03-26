using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using LearnEase_Api.Entity;

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

        public async Task<BaseResponse<bool>> CreateVideoLessonAsync(VideoLessonCreationRequest videoRequest)
        {
            // Kiểm tra null
            if (videoRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", "Dữ liệu bài học không hợp lệ.");

            // Kiểm tra Course có tồn tại không
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var videoLesson = _mapper.Map<VideoLesson>(videoRequest);
                videoLesson.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.GetRepository<VideoLesson>().CreateAsync(videoLesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học được tạo thành công.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài học.");
            }
        }

        public Task<BaseResponse<bool>> DeleteVideoLessonAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<VideoLesson>> GetVideoLessonByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<VideoLesson>>> GetVideoLessonsAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateVideoLessonAsync(Guid id, VideoLessonCreationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
