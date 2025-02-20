using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase_Api.Entity
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
        public string LessonType { get; set; } // Video, Lý thuyết, Bài tập, Hội thoại

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public VideoLesson VideoLesson { get; set; }
        public TheoryLesson TheoryLesson { get; set; }
        public UserProgress UserProgress { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

    }

}
