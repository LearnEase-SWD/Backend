using LearnEase.Core.Entities;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IExerciseService
    {

        Task<IEnumerable<Exercise>> GetExercisesAsync(int pageIndex, int pageSize);
        Task<Exercise?> GetExerciseByIdAsync(Guid id);
        Task CreateExerciseAsync(Exercise exercise);
        Task<bool> UpdateExerciseAsync(Guid id, Exercise exercise);
        Task<bool> DeleteExerciseAsync(Guid id);

    }
}
