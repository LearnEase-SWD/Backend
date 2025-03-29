using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Core.Entities
{
    public class UserVideoLesson
    {
        [Key]
        public Guid UserVideoLessonID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("VideoLesson")]
        public Guid VideoLessonID { get; set; }
        public VideoLesson VideoLesson { get; set; }

        public DateTime? LastAccessedAt { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}