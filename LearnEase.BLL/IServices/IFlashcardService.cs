using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase.Service.IServices
{
	public interface IFlashcardService
	{
		Task<BaseResponse<IEnumerable<FlashcardResponse>>> GetFlashcardsAsync(int pageIndex, int pageSize);
		Task<BaseResponse<FlashcardResponse>> GetFlashcardByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateFlashcardAsync(FlashcardRequest flashcard);
		Task<BaseResponse<bool>> UpdateFlashcardAsync(Guid id, FlashcardRequest flashcard);
		Task<BaseResponse<bool>> DeleteFlashcardAsync(Guid id);
	}
}
