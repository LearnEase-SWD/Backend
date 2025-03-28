using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;

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

        public async Task<BaseResponse<IEnumerable<Lesson>>> GetLessonsAsync(int pageIndex, int pageSize)
        {
            try
            {
                var lessonRepository = _unitOfWork.GetRepository<Lesson>();
                var paginatedLessons = await lessonRepository.GetPagging(lessonRepository.Entities, pageIndex, pageSize);

                return new BaseResponse<IEnumerable<Lesson>>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    paginatedLessons.Items,
                    "Lấy danh sách bài học thành công."
                );
            }
            catch (Exception)
            {
                return new BaseResponse<IEnumerable<Lesson>>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    "Lỗi hệ thống khi lấy danh sách bài học."
                );
            }
        }

        public async Task<BaseResponse<Lesson>> GetLessonByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new BaseResponse<Lesson>(StatusCodeHelper.BadRequest, "INVALID_ID", "ID không hợp lệ.");

            try
            {
                var lesson = await _unitOfWork.GetRepository<Lesson>().GetByIdAsync(id);
                if (lesson == null)
                    return new BaseResponse<Lesson>(StatusCodeHelper.BadRequest, "NOT_FOUND", "Bài học không tồn tại.");

                return new BaseResponse<Lesson>(StatusCodeHelper.OK, "SUCCESS", lesson, "Lấy bài học thành công.");
            }
            catch (Exception)
            {
                return new BaseResponse<Lesson>(StatusCodeHelper.ServerError, "ERROR", "Lỗi hệ thống khi lấy bài học.");
            }
        }

        public async Task<BaseResponse<bool>> CreateLessonAsync(LessonCreateRequest lessonRequest)
        {
            // Kiểm tra null
            if (lessonRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", "Dữ liệu bài học không hợp lệ.");

            // Kiểm tra Course có tồn tại không
            

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
            catch (Exception ex)
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
                var existingLesson = await lessonRepository.GetByIdAsync(id);

                if (existingLesson == null)
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", "Không tìm thấy bài học.");

                await lessonRepository.DeleteAsync(existingLesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được xóa.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài học.");
            }
        }


    }
}
