using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Core.Entities
{
    public class UserTheoryLesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserTheoryLessonID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("TheoryLesson")]
        public Guid TheoryLessonID { get; set; }
        public TheoryLesson TheoryLesson { get; set; }

        public DateTime? LastAccessedAt { get; set; } // Lần cuối truy cập lý thuyết
        public bool IsCompleted { get; set; } = false; // Đã đọc xong lý thuyết chưa?
       
    }
}