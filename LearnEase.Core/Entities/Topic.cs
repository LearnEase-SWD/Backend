using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
	public class Topic
	{
		[Key]
		public Guid TopicID { get; set; }

		[Required]
		[MaxLength(255)]
		public string Name { get; set; } = string.Empty;

		public ICollection<Course> Courses { get; set; }
	}
}
