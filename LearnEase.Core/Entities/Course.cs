using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Core.Entities
{
	public class Course
	{
		[Key]
		public Guid CourseID { get; set; }

		// 🔹 Thêm khóa ngoại đến Topic
		[Required]
		[ForeignKey("Topic")]
		public Guid TopicID { get; set; }
		public Topic Topic { get; set; }

		[Required]
		[MaxLength(255)]
		public string Title { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int TotalLessons { get; set; } = 0;

		[Required]
		[MaxLength(50)]
		public string DifficultyLevel { get; set; } // Beginner, Intermediate, Advanced 

		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
		public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
	}
}
