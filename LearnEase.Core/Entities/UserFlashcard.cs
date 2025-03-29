using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
    public class UserFlashcard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserFlashcardID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Flashcard")]
        public Guid FlashcardID { get; set; }
        public Flashcard Flashcard { get; set; }

        public DateTime? LastReviewedAt { get; set; } // Lần cuối xem lại flashcard (tùy chọn)
        public int ReviewCount { get; set; } = 0; // Số lần xem lại flashcard (tùy chọn)

        [ForeignKey("Lesson")]
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }
    }
}
