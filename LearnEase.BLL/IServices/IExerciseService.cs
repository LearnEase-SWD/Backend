using LearnEase.Core.Base;
using LearnEase.Core.Entities;

namespace LearnEase_Api.LearnEase.Core.IServices
{
	public interface IExerciseService
	{
		Task<BaseResponse<IEnumerable<Exercise>>> GetExercisesAsync(int pageIndex, int pageSize);
		Task<BaseResponse<Exercise>> GetExerciseByIdAsync(Guid id);
		Task<BaseResponse<bool>> CreateExerciseAsync(Exercise exercise);
		Task<BaseResponse<bool>> UpdateExerciseAsync(Guid id, Exercise exercise);
		Task<BaseResponse<bool>> DeleteExerciseAsync(Guid id);
	}
}
