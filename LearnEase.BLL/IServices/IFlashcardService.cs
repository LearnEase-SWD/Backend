using LearnEase.Core.Base;
using LearnEase.Core.Entities;

namespace LearnEase.Service.IServices
{
	public interface IFlashcardService
	{
		Task<BaseResponse<IEnumerable<Flashcard>>> GetFlashcardsAsync(int pageIndex, int pageSize);
		Task<BaseResponse<Flashcard>> GetFlashcardByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateFlashcardAsync(Flashcard flashcard);
		Task<BaseResponse<bool>> UpdateFlashcardAsync(Guid id, Flashcard flashcard);
		Task<BaseResponse<bool>> DeleteFlashcardAsync(Guid id);
	}
}
