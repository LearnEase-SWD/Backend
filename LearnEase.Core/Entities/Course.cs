using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Course
    {
        [Key]
        public Guid CourseID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }


        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public int TotalLessons { get; set; } = 0;

        [Required]
        [MaxLength(50)]
        public string DifficultyLevel { get; set; } // Beginner, Intermediate, Advanced 

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}
