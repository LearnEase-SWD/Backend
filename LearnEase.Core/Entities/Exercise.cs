using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
	public class Exercise
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ExerciseID { get; set; }

		[ForeignKey("Lesson")]
		public Guid LessonID { get; set; }
		public Lesson Lesson { get; set; }

		[Required]
		[MaxLength(50)]
		public string Type { get; set; } // Trắc nghiệm, Điền từ, Sắp xếp câu, ...

		[Required]
		public string Question { get; set; }

		[Required]
		public string AnswerOptions { get; set; }

		[Required]
		public string CorrectAnswer { get; set; }
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<UserExercise> UserExercises { get; set; } = new List<UserExercise>();
	}
}
