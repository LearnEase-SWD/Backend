using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class UserFlashcard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserFlashcardID { get; set; }

        [Required]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        public Guid FlashcardID { get; set; }
        public Flashcard Flashcard { get; set; }

        [Required]
        [MaxLength(20)]
        public string ProficiencyLevel { get; set; } // Beginner, Intermediate, Advanced

        public int ReviewCount { get; set; } = 0; 

        public DateTime? LastReviewedAt { get; set; } = DateTime.UtcNow; 
    }

}
