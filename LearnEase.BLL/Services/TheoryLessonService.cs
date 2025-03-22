using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using LearnEase_Api.Entity;
using Microsoft.Extensions.Logging;

namespace LearnEase.Service.Services
{
    public class TheoryLessonService : ITheoryLessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TheoryLessonService> _logger;

        public TheoryLessonService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TheoryLessonService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> CreateTheoryLessonAsync(TheoryLessonCreationRequest theoryLessonRequest)
        {
            if (theoryLessonRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

            // Kiểm tra lesson có tồn tại không
            var lessonRepository = _unitOfWork.GetRepository<Lesson>();
            Lesson lesson = await lessonRepository.GetByIdAsync(theoryLessonRequest.LessonID);
            if (lesson == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "ID bài học không tồn tại.");

            // Kiểm tra có theory lesson nào đã tồn tại trong 1 lesson chưa 
            var existedTheory = _unitOfWork.GetCustomRepository<ITheoryLessonRepository>()
                                           .GetTheoryByLessonId(theoryLessonRequest.LessonID);
            if (existedTheory == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Bài học đã tồn tại.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var theoryLesson = _mapper.Map<TheoryLesson>(theoryLessonRequest);
                theoryLesson.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.GetRepository<TheoryLesson>().CreateAsync(theoryLesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được tạo thành công.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError($"Lỗi khi tạo bài học: {ex.Message}");
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài học.");
            }
        }

        public async Task<BaseResponse<IEnumerable<TheoryLesson>>> GetTheoryLessonsAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            try
            {
                var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
                var query = theoryLessonRepository.Entities;
                var theoryLessons = await theoryLessonRepository.GetPagging(query, pageIndex, pageSize);

                return new BaseResponse<IEnumerable<TheoryLesson>>(
                    StatusCodeHelper.OK,
                    "SUCCESS",
                    theoryLessons.Items,
                    "Lấy danh sách bài học thành công."
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy danh sách bài học: {ex.Message}");
                return new BaseResponse<IEnumerable<TheoryLesson>>(
                    StatusCodeHelper.ServerError,
                    "ERROR",
                    new List<TheoryLesson>(),
                    "Lỗi hệ thống khi lấy danh sách bài học."
                );
            }
        }

        public async Task<BaseResponse<TheoryLesson>> GetTheoryLessonByIdAsync(Guid id)
        {
            try
            {
                var theoryLesson = await _unitOfWork.GetRepository<TheoryLesson>().GetByIdAsync(id);
                if (theoryLesson == null)
                    return new BaseResponse<TheoryLesson>(StatusCodeHelper.BadRequest, "NOT_FOUND", null, "Bài học không tồn tại.");

                return new BaseResponse<TheoryLesson>(StatusCodeHelper.OK, "SUCCESS", theoryLesson, "Lấy bài học thành công.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy bài học với ID {id}: {ex.Message}");
                return new BaseResponse<TheoryLesson>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài học.");
            }
        }

        public async Task<BaseResponse<bool>> UpdateTheoryLessonAsync(Guid id, TheoryLessonCreationRequest request)
        {
            if (request == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
                var existingLesson = await theoryLessonRepository.GetByIdAsync(id);

                if (existingLesson == null)
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài học.");

                _mapper.Map(request, existingLesson);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được cập nhật.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError($"Lỗi khi cập nhật bài học {id}: {ex.Message}");
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi cập nhật bài học.");
            }
        }

        public async Task<BaseResponse<bool>> DeleteTheoryLessonAsync(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
                var existingLesson = await theoryLessonRepository.GetByIdAsync(id);

                if (existingLesson == null)
                    return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "Không tìm thấy bài học.");

                await theoryLessonRepository.DeleteAsync(existingLesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được xóa.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError($"Lỗi khi xóa bài học {id}: {ex.Message}");
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài học.");
            }
        }
    }
}
