using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Reponse;
using LearnEase.Core.Models.Request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
	public interface IExerciseService
	{
		Task<BaseResponse<IEnumerable<ExerciseResponse>>> GetExercisesAsync(int pageIndex, int pageSize);
		Task<BaseResponse<ExerciseResponse>> GetExerciseByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateExerciseAsync(ExerciseRequest exercise);
		Task<BaseResponse<bool>> UpdateExerciseAsync(Guid id, ExerciseRequest exercise);
		Task<BaseResponse<bool>> DeleteExerciseAsync(Guid id);
	}
}
