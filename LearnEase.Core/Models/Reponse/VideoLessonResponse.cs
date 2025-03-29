using LearnEase.Core.Enum;

namespace LearnEase.Core.Models.Reponse
{
	public class VideoLessonResponse
	{
		public Guid VideoID { get; set; }
		public Guid LessonID { get; set; }
		public LessonTypeEnum LessonType { get; set; }
		public string VideoURL { get; set; }
		public TimeSpan Duration { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
