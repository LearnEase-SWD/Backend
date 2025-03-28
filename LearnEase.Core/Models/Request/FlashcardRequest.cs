using LearnEase.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Core.Models.Request
{
	public class FlashcardRequest
	{
		[Required(ErrorMessage = "LessonID cannot be empty!")]
		public Guid LessonID { get; set; }

		[Required(ErrorMessage = "Front cannot be empty!")]
		public string Front { get; set; }

		[Required(ErrorMessage = "Back cannot be empty!")]
		public string Back { get; set; }

		public string? PronunciationAudioURL { get; set; }
	}
}
