using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Core.DTO
{
	public class FlashcardDTO
	{
		public Guid FlashcardID { get; set; }
		public Guid LessonID { get; set; }
		public string Front { get; set; }
		public string Back { get; set; }
		public string PronunciationAudioURL { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
