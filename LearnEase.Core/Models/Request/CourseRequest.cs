using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
	public class CourseRequest
	{
		public Guid? TopicID { get; set; }

		[Required(ErrorMessage = "Title cannot be empty!")]
		[MaxLength(255)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Price cannot be empty!")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "TotalLessons cannot be empty!")]
		public int TotalLessons { get; set; } = 0;
		public string Description { get; set; }

		[Required]
		[MaxLength(50)]
		public string Status { get; set; }

		[Url]
		public string? Url { get; set; }

		[Required]
		[MaxLength(50)]
		public string DifficultyLevel { get; set; }
	}
}
