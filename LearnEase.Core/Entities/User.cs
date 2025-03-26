using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public enum UserRole
    {
        Admin =1,
       
        Student =2
    }   
    public class User
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public bool IsActive { get; set; } = true;
        public string CreatedAt { get; set; }

        public UserDetail UserDetail { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.Student;
        public ICollection<UserLesson> UserProgresses { get; set; } = new List<UserLesson>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public ICollection<UserExercise> UserExercises { get; set; } = new List<UserExercise>();
        public ICollection<UserFlashcard> UserFlashcards { get; set; } = new List<UserFlashcard>(); 
    }
}