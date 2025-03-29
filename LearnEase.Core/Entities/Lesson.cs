using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LearnEase.Core.Enum;

namespace LearnEase.Core.Entities
{
    public class Lesson
    {
        [Key]
        public Guid LessonID { get; set; }

        [ForeignKey("Course")]
        public Guid CourseID { get; set; }
        public Course Course { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public LessonTypeEnum LessonType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<VideoLesson> VideoLessons { get; set; } = new List<VideoLesson>();
        public ICollection<TheoryLesson> TheoryLessons { get; set; } = new List<TheoryLesson>();
        public UserLesson UserProgress { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

    }

}
