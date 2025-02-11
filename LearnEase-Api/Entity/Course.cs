using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase_Api.Entity
{
    public class Course
    {
        [Key]
        public Guid CourseID { get; set; }

        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; }

        [MaxLength(1000)]
        public string CourseDescription { get; set; }

        [Required]
        [MaxLength(100)]
        public string Language { get; set; }

        [Required]
        [MaxLength(50)]
        public string DifficultyLevel { get; set; } // Beginner, Intermediate, Advanced 

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Forum Forum { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}
