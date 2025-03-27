using LearnEase.Core.Entities;

namespace LearnEase.Repository.IRepository
{
    public interface IFlashcardService 
    {
        Task<IEnumerable<Flashcard>> GetFlashcardsAsync(int pageIndex, int pageSize);
        Task<Flashcard?> GetFlashcardByIdAsync(Guid id);
        Task CreateFlashcardAsync(Flashcard flashcard);
        Task<bool> UpdateFlashcardAsync(Guid id, Flashcard flashcard);
        Task<bool> DeleteFlashcardAsync(Guid id);
    }
}
