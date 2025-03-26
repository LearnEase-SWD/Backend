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
        public string? Progress { get; set; }
    }

}
