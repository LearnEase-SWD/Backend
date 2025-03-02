using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
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

        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }

        public UserDetail UserDetail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
        public ICollection<UserExercise> UserExercises { get; set; } = new List<UserExercise>();
        public ICollection<UserFlashcard> UserFlashcards { get; set; } = new List<UserFlashcard>(); 
    }
}