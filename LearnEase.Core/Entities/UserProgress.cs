using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class UserProgress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProgressID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        [MaxLength(50)]
        public string CompletionStatus { get; set; } // In Progress, Completed

        public int Score { get; set; }

        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
    }
}
