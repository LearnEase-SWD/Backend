using LearnEase.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
    public class VideoLessonCreateRequest
    {
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }
        public string VideoURL { get; set; }
        public string Transcript { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
