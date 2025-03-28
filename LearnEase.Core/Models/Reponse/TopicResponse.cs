namespace LearnEase.Core.Models.Reponse
{
    public class TopicResponse
    {
		public Guid TopicId { get; set; }
		public string Name { get; set; }
		public List<CourseResponse> Courses { get; set; }

	}
}
