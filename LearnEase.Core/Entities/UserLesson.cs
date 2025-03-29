using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Entities
{
    public class UserLesson
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
        [Range(0, 100)]
        public int Progress { get; set; }  // 0% , 20%, 50%, ...

        public bool IsVideoCompleted { get; set; } = false; // Đã xem xong video chưa?
        public bool IsExerciseCompleted { get; set; } = false; // Đã làm xong bài tập chưa?
        public bool HasAccessedFlashcards { get; set; } = false;
        public bool IsTheoryCompleted { get; set; } = false; // Đã đọc xong lý thuyết chưa?

        public DateTime StartedAt { get; set; } = DateTime.UtcNow; // Thời điểm bắt đầu Lesson
        public DateTime? CompletedAt { get; set; } // Thời điểm hoàn thành Lesson
        public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
    }
}
