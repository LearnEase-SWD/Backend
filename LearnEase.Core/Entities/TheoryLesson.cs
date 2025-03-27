using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
    public class TheoryLesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TheoryID { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        public string Content { get; set; }

        public string Examples { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
