namespace LearnEase.Core.Models.Request
{
	public class UserCourseRequest
	{
		public string UserId { get; set; }
		public Guid CourseID { get; set; }
		public string ProgressStatus { get; set; }
		public int ProgressPercentage { get; set; }
		public DateTime? CompletedAt { get; set; }
	}
}
