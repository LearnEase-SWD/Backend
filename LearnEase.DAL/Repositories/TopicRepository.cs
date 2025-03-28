using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
	public class TopicRepository : ITopicRepository
	{
		private readonly ApplicationDbContext _context;

		public TopicRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<TopicResponse> GetCourseByTopicAsync(Guid topicId, int pageIndex, int pageSize)
		{
			try
			{
				var topic = await _context.Topic
					.Include(t => t.Courses)
					.Where(t => t.TopicID == topicId)
					.FirstOrDefaultAsync();

				if (topic == null)
				{
					return new TopicResponse
					{
						Name = string.Empty,
						Courses = new List<CourseResponse>(),
					};
				}

				// Lấy danh sách Courses với phân trang
				var courses = topic.Courses
					.Skip((pageIndex - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				// Mapping dữ liệu
				var courseResponses = courses.Select(c => new CourseResponse
				{
					CourseID = c.CourseID,
					TopicName = topic.Name,
					Title = c.Title,
					Price = c.Price,
					Description = c.Description,
					Status = c.Status,
					Url = c.Url,
					TotalLessons = c.TotalLessons,
					DifficultyLevel = c.DifficultyLevel,
					UpdatedAt = c.UpdatedAt,
					CreatedAt = c.CreatedAt
				}).ToList();

				return new TopicResponse
				{
					Name = topic.Name,
					Courses = courseResponses
				};
			}
			catch (Exception)
			{
				return new TopicResponse
				{
					Name = "Error",
					Courses = new List<CourseResponse>(),
				};
			}
		}
	}
}
