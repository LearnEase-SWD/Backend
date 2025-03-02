using LearnEase.Core;

using LearnEase_Api.Entity;

namespace LearnEase.Repository.IRepository
{
    public interface IFlashcardService 
    {
        Task<IEnumerable<Flashcard>> GetAllFlashcardsAsync();
        Task<Flashcard?> GetFlashcardByIdAsync(Guid id);
        Task CreateFlashcardAsync(Flashcard flashcard);
        Task<bool> UpdateFlashcardAsync(Guid id, Flashcard flashcard);
        Task<bool> DeleteFlashcardAsync(Guid id);
    }
}
