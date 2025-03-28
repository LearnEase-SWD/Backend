namespace LearnEase.Core.Models.Reponse
{
	public class UserCourseResponse
	{
		public Guid UserCourseID { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public Guid CourseID { get; set; }
		public string CourseName { get; set; }
		public string ProgressStatus { get; set; }
		public int ProgressPercentage { get; set; }
		public DateTime? CompletedAt { get; set; }
		public DateTime EnrolledAt { get; set; }
	}
}
