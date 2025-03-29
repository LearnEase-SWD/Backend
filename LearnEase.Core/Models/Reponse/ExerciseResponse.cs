using LearnEase.Core.Enum;

namespace LearnEase.Core.Models.Reponse
{
    public class ExerciseResponse
    {
        public Guid ExerciseID { get; set; }
        public Guid LessonID { get; set; }
        public string Type { get; set; }
		public LessonTypeEnum LessonType { get; set; }
		public string Question { get; set; }
        public string AnswerOptions { get; set; }
        public string CorrectAnswer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
