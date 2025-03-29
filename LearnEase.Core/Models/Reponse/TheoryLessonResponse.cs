using LearnEase.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LearnEase.Core.Enum;

namespace LearnEase.Core.Models.Reponse
{
	public class TheoryLessonResponse
	{
		public Guid TheoryID { get; set; }
		public Guid LessonID { get; set; }
		public LessonTypeEnum LessonType { get; set; }
		public string Content { get; set; }
		public string Examples { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
