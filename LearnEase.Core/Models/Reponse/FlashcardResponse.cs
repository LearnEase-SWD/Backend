using LearnEase.Core.Enum;

namespace LearnEase.Core.Models.Reponse
{
    public class FlashcardResponse
    {
        public Guid FlashcardID { get; set; }
        public Guid LessonID { get; set; }
        public LessonTypeEnum LessonType { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public string PronunciationAudioURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
