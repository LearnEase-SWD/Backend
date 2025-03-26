using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Flashcard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FlashcardID { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        public string Front { get; set; }

        [Required]
        public string Back { get; set; } 

        public string? PronunciationAudioURL { get; set; }

      

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<UserFlashcard> UserFlashcards { get; set; } = new List<UserFlashcard>();
    }

}
