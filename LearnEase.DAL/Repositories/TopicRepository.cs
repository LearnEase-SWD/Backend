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
						CourseIds = new List<Guid>()
					};
				}

				// Lấy danh sách CourseId với phân trang
				var courseIds = topic.Courses
					.Skip((pageIndex - 1) * pageSize)
					.Take(pageSize)
					.Select(c => c.CourseID)
					.ToList();

				return new TopicResponse
				{
					TopicId = topic.TopicID,
					Name = topic.Name,
					CourseIds = courseIds
				};
			}
			catch (Exception)
			{
				return new TopicResponse
				{
					Name = "Error",
					CourseIds = new List<Guid>()
				};
			}
		}
	}
}
