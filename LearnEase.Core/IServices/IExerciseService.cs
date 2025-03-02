using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IExerciseService
    {

        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise?> GetExerciseByIdAsync(Guid id);
        Task CreateExerciseAsync(Exercise exercise);
        Task<bool> UpdateExerciseAsync(Guid id, Exercise exercise);
        Task<bool> DeleteExerciseAsync(Guid id);

    }
}
