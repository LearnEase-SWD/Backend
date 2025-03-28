using AutoMapper;
using LearnEase.Core;
using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.UOW;
using LearnEase.Service.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service.Services
{
	public class TopicService : ITopicService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<TopicService> _logger;

		public TopicService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TopicService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<BaseResponse<IEnumerable<TopicResponse>>> GetTopicsAsync(int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var topicRepository = _unitOfWork.GetRepository<Topic>();
				var topicCustomRepository = _unitOfWork.GetCustomRepository<ITopicRepository>();

				// Lấy danh sách chủ đề
				var query = topicRepository.Entities;
				var topics = await topicRepository.GetPaggingAsync(query, pageIndex, pageSize);
				var topicResponses = _mapper.Map<IEnumerable<TopicResponse>>(topics.Items);

				// Lấy danh sách khoá học cho từng chủ đề
				foreach (var topic in topicResponses)
				{
					var courseResult = await topicCustomRepository.GetCourseByTopicAsync(topic.TopicId, 1, int.MaxValue);

					// Đảm bảo Courses không null trước khi add
					topic.Courses = _mapper.Map<List<CourseResponse>>(courseResult.Courses);
				}

				return new BaseResponse<IEnumerable<TopicResponse>>(
					StatusCodeHelper.OK,
					"SUCCESS",
					topicResponses,
					"Lấy danh sách chủ đề thành công."
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy danh sách chủ đề: {ex.Message}");
				return new BaseResponse<IEnumerable<TopicResponse>>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					"Lỗi hệ thống khi lấy danh sách chủ đề."
				);
			}
		}

		public async Task<BaseResponse<TopicResponse>> GetCoursesByTopicAsync(Guid topicId, int pageIndex, int pageSize)
		{
			if (pageIndex < 1) pageIndex = 1;
			if (pageSize < 1) pageSize = 10;

			try
			{
				var topicRepository = _unitOfWork.GetCustomRepository<ITopicRepository>();
				var topicResponse = await topicRepository.GetCourseByTopicAsync(topicId, pageIndex, pageSize);

				if (topicResponse == null || string.IsNullOrEmpty(topicResponse.Name))
				{
					return new BaseResponse<TopicResponse>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						null,
						"Không tìm thấy chủ đề hoặc không có khóa học nào trong chủ đề này."
					);
				}

				return new BaseResponse<TopicResponse>(
					StatusCodeHelper.OK,
					"SUCCESS",
					topicResponse,
					"Lấy danh sách khóa học theo chủ đề thành công."
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy danh sách khóa học theo chủ đề {topicId}: {ex.Message}");
				return new BaseResponse<TopicResponse>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					"Lỗi hệ thống khi lấy danh sách khóa học theo chủ đề."
				);
			}
		}

		public async Task<BaseResponse<TopicResponse>> GetTopicByIdAsync(Guid id)
		{
			try
			{
				var topic = await _unitOfWork.GetRepository<Topic>().GetByIdAsync(id);

				if (topic == null)
					return new BaseResponse<TopicResponse>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						null,
						"Chủ đề không tồn tại."
					);

				var topicResponse = _mapper.Map<TopicResponse>(topic);

				return new BaseResponse<TopicResponse>(
					StatusCodeHelper.OK,
					"SUCCESS",
					topicResponse,
					"Lấy chủ đề thành công."
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Lỗi khi lấy chủ đề với ID {id}: {ex.Message}");
				return new BaseResponse<TopicResponse>(
					StatusCodeHelper.ServerError,
					"ERROR",
					null,
					"Lỗi hệ thống khi lấy chủ đề."
				);
			}
		}

		public async Task<BaseResponse<bool>> CreateTopicAsync(string topicName)
		{
			if (string.IsNullOrWhiteSpace(topicName))
				return new BaseResponse<bool>(
					StatusCodeHelper.BadRequest,
					"INVALID_REQUEST",
					false,
					"Tên chủ đề không hợp lệ."
				);

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var topic = new Topic
				{
					TopicID = Guid.NewGuid(),
					Name = topicName
				};

				await _unitOfWork.GetRepository<Topic>().CreateAsync(topic);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(
					StatusCodeHelper.OK,
					"SUCCESS",
					true,
					"Chủ đề được tạo thành công."
				);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi tạo chủ đề: {ex.Message}");
				return new BaseResponse<bool>(
					StatusCodeHelper.ServerError,
					"ERROR",
					false,
					"Lỗi hệ thống khi tạo chủ đề."
				);
			}
		}

		public async Task<BaseResponse<bool>> UpdateTopicAsync(Guid id, string topicName)
		{
			if (string.IsNullOrWhiteSpace(topicName))
				return new BaseResponse<bool>(
					StatusCodeHelper.BadRequest,
					"INVALID_REQUEST",
					false,
					"Tên chủ đề không hợp lệ."
				);

			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var topicRepository = _unitOfWork.GetRepository<Topic>();
				var existingTopic = await topicRepository.GetByIdAsync(id);

				if (existingTopic == null)
					return new BaseResponse<bool>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						false,
						"Không tìm thấy chủ đề."
					);

				existingTopic.Name = topicName;

				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(
					StatusCodeHelper.OK,
					"SUCCESS",
					true,
					"Chủ đề đã được cập nhật."
				);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi cập nhật chủ đề {id}: {ex.Message}");
				return new BaseResponse<bool>(
					StatusCodeHelper.ServerError,
					"ERROR",
					false,
					"Lỗi hệ thống khi cập nhật chủ đề."
				);
			}
		}

		public async Task<BaseResponse<bool>> DeleteTopicAsync(Guid id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var topicRepository = _unitOfWork.GetRepository<Topic>();
				var existingTopic = await topicRepository.GetByIdAsync(id);

				if (existingTopic == null)
					return new BaseResponse<bool>(
						StatusCodeHelper.BadRequest,
						"NOT_FOUND",
						false,
						"Không tìm thấy chủ đề."
					);

				await topicRepository.DeleteAsync(existingTopic);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();

				return new BaseResponse<bool>(
					StatusCodeHelper.OK,
					"SUCCESS",
					true,
					"Chủ đề đã được xóa."
				);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				_logger.LogError($"Lỗi khi xóa chủ đề {id}: {ex.Message}");
				return new BaseResponse<bool>(
					StatusCodeHelper.ServerError,
					"ERROR",
					false,
					"Lỗi hệ thống khi xóa chủ đề."
				);
			}
		}
	}
}
