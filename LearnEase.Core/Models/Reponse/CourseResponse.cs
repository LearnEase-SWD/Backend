namespace LearnEase.Core.Models.Reponse
{
    public class CourseResponse
    {
        public Guid CourseID { get; set; }
		public Guid TopicId { get; set; }
		public string TopicName { get; set; }
		public string Title { get; set; }
        public decimal Price { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string Status { get; set; }
		public int TotalLessons { get; set; }
        public string DifficultyLevel { get; set; }
        public IEnumerable<LessonResponse> Lessons { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
