using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
	public class UserLesson
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ProgressID { get; set; }

		[ForeignKey("User")]
		public string UserID { get; set; }
		public User User { get; set; }

		[ForeignKey("Lesson")]
		public Guid LessonID { get; set; }
		public Lesson Lesson { get; set; }

		[Required]
		[Range(0, 100)]
		public int Progress { get; set; }  // 0% , 20%, 50%, ...

		public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
	}
}
