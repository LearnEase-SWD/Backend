using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.DTO
{
	public class ExerciseDTO
	{
		public Guid ExerciseID { get; set; }
		public Guid LessonID { get; set; }
		public string Type { get; set; }
		public string Question { get; set; }
		public string AnswerOptions { get; set; }
		public string CorrectAnswer { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
