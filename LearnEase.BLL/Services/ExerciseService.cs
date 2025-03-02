using LearnEase.Repository.IRepository;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static LearnEase_Api.LearnEase.Core.Services.ExerciseService;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _exerciseRepository.Entities.ToListAsync();
        }

        public async Task<Exercise?> GetExerciseByIdAsync(Guid id)
        {
            return await _exerciseRepository.GetByIdAsync(id);
        }

        public async Task CreateExerciseAsync(Exercise exercise)
        {
            await _exerciseRepository.CreateAsync(exercise);
            await _exerciseRepository.SaveAsync();
        }

        public async Task<bool> UpdateExerciseAsync(Guid id, Exercise exercise)
        {
            var existingExercise = await _exerciseRepository.GetByIdAsync(id);
            if (existingExercise == null) return false;

            existingExercise.Type = exercise.Type;
            existingExercise.Question = exercise.Question;
            existingExercise.CorrectAnswer = exercise.CorrectAnswer;
            existingExercise.LessonID = exercise.LessonID;
            existingExercise.CreatedAt = DateTime.UtcNow;

            await _exerciseRepository.UpdateAsync(existingExercise);
            await _exerciseRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            var existingExercise = await _exerciseRepository.GetByIdAsync(id);
            if (existingExercise == null) return false;

            await _exerciseRepository.DeleteAsync(id);
            await _exerciseRepository.SaveAsync();
            return true;
        }


    }
}

