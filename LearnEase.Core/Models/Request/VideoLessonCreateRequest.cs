using LearnEase.Core.Entities;

namespace LearnEase.Core.Models.Request
{
    public class VideoLessonCreateRequest
    {
        public Guid LessonID { get; set; }
        public string VideoURL { get; set; }
        public string Transcript { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
