using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class UserCourse
    {
        [Key]
        public Guid UserCourseID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Course")]
        public Guid CourseID { get; set; }
        public Course Course { get; set; } 

        [Required]
        [MaxLength(50)]
        public string ProgressStatus { get; set; }

        public int ProgressPercentage { get; set; } = 0;

        public DateTime? CompletedAt { get; set; }
		public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

	}
}
