using LearnEase.Repository.IRepository;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearnEase_Api.LearnEase.Core.Services
{

    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepository;

        public FlashcardService(IFlashcardRepository flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        public async Task<IEnumerable<Flashcard>> GetAllFlashcardsAsync()
        {
            return await _flashcardRepository.Entities.ToListAsync();
        }

        public async Task<Flashcard?> GetFlashcardByIdAsync(Guid id)
        {
            return await _flashcardRepository.GetByIdAsync(id);
        }

        public async Task CreateFlashcardAsync(Flashcard flashcard)
        {
            await _flashcardRepository.CreateAsync(flashcard);
            await _flashcardRepository.SaveAsync();
        }

        public async Task<bool> UpdateFlashcardAsync(Guid id, Flashcard flashcard)
        {
            var existingFlashcard = await _flashcardRepository.GetByIdAsync(id);
            if (existingFlashcard == null) return false;

            existingFlashcard.Front = flashcard.Front;
            existingFlashcard.Back = flashcard.Back;
            existingFlashcard.PronunciationAudioURL = flashcard.PronunciationAudioURL;
            existingFlashcard.Topic = flashcard.Topic;
            existingFlashcard.LessonID = flashcard.LessonID;
            existingFlashcard.CreatedAt = DateTime.UtcNow;

            await _flashcardRepository.UpdateAsync(existingFlashcard);
            await _flashcardRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteFlashcardAsync(Guid id)
        {
            var existingFlashcard = await _flashcardRepository.GetByIdAsync(id);
            if (existingFlashcard == null) return false;

            await _flashcardRepository.DeleteAsync(id);
            await _flashcardRepository.SaveAsync();
            return true;
        }
    }
}
           
    
