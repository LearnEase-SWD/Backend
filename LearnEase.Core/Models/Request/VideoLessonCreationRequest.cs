using LearnEase_Api.Entity;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
    public class VideoLessonCreationRequest
    {
        public Guid LessonID { get; set; }
        public Lesson Lesson { get; set; }
        public string VideoURL { get; set; }
        public string Transcript { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
