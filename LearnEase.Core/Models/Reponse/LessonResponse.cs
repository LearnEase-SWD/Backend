using LearnEase.Core.Enum;

namespace LearnEase.Core.Models.Reponse
{
	public class LessonResponse
	{
		public Guid LessonID { get; set; }
		public Guid CourseID { get; set; }
		public int Index { get; set; }
		public string Title { get; set; }
		public LessonTypeEnum LessonType { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
