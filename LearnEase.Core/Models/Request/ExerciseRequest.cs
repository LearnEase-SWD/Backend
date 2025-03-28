using LearnEase.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
	public class ExerciseRequest
	{
		public Guid LessonID { get; set; }

		[Required(ErrorMessage = "Type cannot be empty!")]
		[MaxLength(50)]
		public string Type { get; set; }

		[Required(ErrorMessage = "Question cannot be empty!")]
		public string Question { get; set; }

		[Required(ErrorMessage = "AnswerOptions cannot be empty!")]
		public string AnswerOptions { get; set; }

		[Required(ErrorMessage = "CorrectAnswer cannot be empty!")]
		public string CorrectAnswer { get; set; }
	}
}
