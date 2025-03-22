using LearnEase.Repository.UOW;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exercise>> GetExercisesAsync(int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GetRepository<Exercise>().Entities;
            var paginatedResult = await _unitOfWork.GetRepository<Exercise>().GetPagging(query, pageIndex, pageSize);
            return paginatedResult.Items;
        }

        public async Task<Exercise?> GetExerciseByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Exercise>().GetByIdAsync(id);
        }

        public async Task CreateExerciseAsync(Exercise exercise)
        {
            await _unitOfWork.GetRepository<Exercise>().CreateAsync(exercise);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateExerciseAsync(Guid id, Exercise exercise)
        {
            var exerciseRepository = _unitOfWork.GetRepository<Exercise>();

            var existingExercise = await exerciseRepository.GetByIdAsync(id);
            if (existingExercise == null) return false;

            existingExercise.Type = exercise.Type;
            existingExercise.Question = exercise.Question;
            existingExercise.CorrectAnswer = exercise.CorrectAnswer;
            existingExercise.LessonID = exercise.LessonID;
            existingExercise.CreatedAt = DateTime.UtcNow;

            await exerciseRepository.UpdateAsync(existingExercise);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            var exerciseRepository = _unitOfWork.GetRepository<Exercise>();
            var existingExercise = await exerciseRepository.GetByIdAsync(id);
            if (existingExercise == null) return false;

            await exerciseRepository.DeleteAsync(id);
            await exerciseRepository.SaveAsync();
            return true;
        }
    }
}
