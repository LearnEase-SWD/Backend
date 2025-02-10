using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
{
    public class VideoLesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VideoID { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        public string VideoURL { get; set; }

        public string Transcript { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
