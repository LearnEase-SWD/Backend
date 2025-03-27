using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.UOW;

namespace LearnEase_Api.LearnEase.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlashcardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Flashcard>> GetFlashcardsAsync(int pageIndex, int pageSize)
        {
            var query = _unitOfWork.GetRepository<Flashcard>().Entities;
            var paginatedResult = await _unitOfWork.GetRepository<Flashcard>().GetPagging(query, pageIndex, pageSize);
            return paginatedResult.Items;
        }

        public async Task<Flashcard?> GetFlashcardByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Flashcard>().GetByIdAsync(id);
        }

        public async Task CreateFlashcardAsync(Flashcard flashcard)
        {
            await _unitOfWork.GetRepository<Flashcard>().CreateAsync(flashcard);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateFlashcardAsync(Guid id, Flashcard flashcard)
        {
            var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
            var existingFlashcard = await flashcardRepository.GetByIdAsync(id);
            if (existingFlashcard == null) return false;

            existingFlashcard.Front = flashcard.Front;
            existingFlashcard.Back = flashcard.Back;
            existingFlashcard.PronunciationAudioURL = flashcard.PronunciationAudioURL;
            
            existingFlashcard.LessonID = flashcard.LessonID;
            existingFlashcard.CreatedAt = DateTime.UtcNow;

            await flashcardRepository.UpdateAsync(existingFlashcard);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteFlashcardAsync(Guid id)
        {
            var flashcardRepository = _unitOfWork.GetRepository<Flashcard>();
            var existingFlashcard = await flashcardRepository.GetByIdAsync(id);
            if (existingFlashcard == null) return false;

            await flashcardRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}

