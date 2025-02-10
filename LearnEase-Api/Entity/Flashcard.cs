using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class Flashcard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FlashcardID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        [Required]
        public string Front { get; set; } // Từ/cụm từ cần học

        [Required]
        public string Back { get; set; } // Nghĩa hoặc giải thích

        public string PronunciationAudioURL { get; set; }

        [MaxLength(100)]
        public string Category { get; set; } // Chủ đề từ vựng

        [MaxLength(50)]
        public string Level { get; set; } // Cấp độ từ vựng

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
