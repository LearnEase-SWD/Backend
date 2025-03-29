using AutoMapper;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;
using LearnEase.Repository.Repositories;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;

namespace LearnEase.Service.Services
{
    public class TheoryLessonService : ITheoryLessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TheoryLessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> CreateTheoryLessonAsync(TheoryLessonCreateRequest theoryLessonRequest)
        {
            if (theoryLessonRequest == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "INVALID_REQUEST", false, "Dữ liệu bài học không hợp lệ.");

            // Kiểm tra lesson có tồn tại không
            var lessonRepository = _unitOfWork.GetRepository<Lesson>();
            Lesson lesson = await lessonRepository.GetByIdAsync(theoryLessonRequest.LessonID);
            if (lesson == null)
                return new BaseResponse<bool>(StatusCodeHelper.BadRequest, "NOT_FOUND", false, "ID bài học không tồn tại.");

			await _unitOfWork.BeginTransactionAsync();

            try
            {
                var theoryLesson = _mapper.Map<TheoryLesson>(theoryLessonRequest);
                theoryLesson.CreatedAt = DateTime.UtcNow;

                // Sửa lessontype
                lesson.LessonType = LessonTypeEnum.Theory;

				await _unitOfWork.GetRepository<TheoryLesson>().CreateAsync(theoryLesson);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học đã được tạo thành công.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi tạo bài học.");
            }
        }

		public async Task<BaseResponse<IEnumerable<TheoryLessonResponse>>> GetTheoryLessonsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				// Lấy danh sách TheoryLesson với phân trang
				var query = theoryLessonRepository.Entities;
				var theoryLessons = await theoryLessonRepository.GetPaggingAsync(query, pageIndex, pageSize);

				// Duyệt từng phần tử và lấy Lesson tương ứng
				var responseList = new List<TheoryLessonResponse>();
				foreach (var tl in theoryLessons.Items)
				{
					// Lấy từng lesson theo ID
					var lesson = await lessonRepository.GetByIdAsync(tl.LessonID);

					responseList.Add(new TheoryLessonResponse
					{
						TheoryID = tl.TheoryID,
						LessonID = tl.LessonID,
						LessonType = (LessonTypeEnum)lesson.LessonType,
						Content = tl.Content,
						Examples = tl.Examples,
						CreatedAt = tl.CreatedAt
					});
				}

				return new BaseResponse<IEnumerable<TheoryLessonResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					responseList,
					"Lấy danh sách bài học thành công."
				);
			}
			catch (Exception)
			{
				return new BaseResponse<IEnumerable<TheoryLessonResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					new List<TheoryLessonResponse>(),
					"Lỗi hệ thống khi lấy danh sách bài học."
				);
			}
		}

		public async Task<BaseResponse<TheoryLessonResponse>> GetTheoryLessonByIdAsync(Guid id)
		{
			try
			{
				var theoryLesson = await _unitOfWork.GetRepository<TheoryLesson>().GetByIdAsync(id);
				var lessonRepository = _unitOfWork.GetRepository<Lesson>();

				if (theoryLesson == null)
					return new BaseResponse<TheoryLessonResponse>(StatusCodeHelper.NotFound, "NOT_FOUND", null, "Bài học không tồn tại.");

				var lesson = await lessonRepository.GetByIdAsync(theoryLesson.LessonID);

				var response = new TheoryLessonResponse
				{
					TheoryID = theoryLesson.TheoryID,
					LessonID = theoryLesson.LessonID,
					LessonType = (LessonTypeEnum)lesson.LessonType,
					Content = theoryLesson.Content,
					Examples = theoryLesson.Examples,
					CreatedAt = theoryLesson.CreatedAt
				};

				return new BaseResponse<TheoryLessonResponse>(StatusCodeHelper.OK, "SUCCESS", response, "Lấy bài học thành công.");
			}
			catch (Exception)
			{
				return new BaseResponse<TheoryLessonResponse>(StatusCodeHelper.ServerError, "ERROR", null, "Lỗi hệ thống khi lấy bài học.");
			}
		}

		public async Task<BaseResponse<bool>> UpdateTheoryLessonAsync(Guid id, TheoryLessonCreateRequest request)
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
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi xóa bài học.");
            }
        }
        public async Task<BaseResponse<bool>> MarkTheoryLessonAsCompletedAsync(string userId, Guid theoryLessonId)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userTheoryLessonRepository = _unitOfWork.GetRepository<UserTheoryLesson>();
                var theoryLessonRepository = _unitOfWork.GetRepository<TheoryLesson>();
                var userLessonRepository = _unitOfWork.GetRepository<UserLesson>();

                // Kiểm tra xem TheoryLesson có tồn tại không
                var theoryLesson = await theoryLessonRepository.GetByIdAsync(theoryLessonId);
                if (theoryLesson == null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.NotFound, "THEORY_LESSON_NOT_FOUND", false, "Bài học lý thuyết không tồn tại.");
                }

                // Tìm UserLesson tương ứng
                var userLesson = await userLessonRepository.FirstOrDefaultAsync(ul => ul.UserID == userId && ul.LessonID == theoryLesson.LessonID);
                if (userLesson == null)
                {
                    return new BaseResponse<bool>(StatusCodeHelper.NotFound, "USER_LESSON_NOT_FOUND", false, "UserLesson không tồn tại");
                }

                // Kiểm tra xem UserTheoryLesson có tồn tại không
                var existingUserTheoryLesson = await userTheoryLessonRepository.FirstOrDefaultAsync(utl => utl.UserID == userId && utl.TheoryLessonID == theoryLessonId);

                if (existingUserTheoryLesson == null)
                {
                    // Nếu chưa tồn tại, tạo mới và đánh dấu là đã hoàn thành
                    var newUserTheoryLesson = new UserTheoryLesson
                    {
                        UserID = userId,
                        TheoryLessonID = theoryLessonId,
                        IsCompleted = true,
                        LastAccessedAt = DateTime.UtcNow
                    };

                    await userTheoryLessonRepository.CreateAsync(newUserTheoryLesson);
                }
                else
                {
                    // Nếu đã tồn tại, cập nhật trạng thái đã hoàn thành
                    existingUserTheoryLesson.IsCompleted = true;
                    existingUserTheoryLesson.LastAccessedAt = DateTime.UtcNow;
                    await userTheoryLessonRepository.UpdateAsync(existingUserTheoryLesson);
                }

                // Cập nhật UserLesson
                userLesson.IsTheoryCompleted = true;
                await userLessonRepository.UpdateAsync(userLesson);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new BaseResponse<bool>(StatusCodeHelper.OK, "SUCCESS", true, "Bài học lý thuyết đã được đánh dấu là hoàn thành.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new BaseResponse<bool>(StatusCodeHelper.ServerError, "ERROR", false, "Lỗi hệ thống khi đánh dấu bài học lý thuyết.");
            }
        }
}
