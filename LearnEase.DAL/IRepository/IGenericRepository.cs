using LearnEase.Core;

namespace LearnEase.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        // Query
        IQueryable<T> Entities { get; }

        Task<BasePaginatedList<T>> GetPagging(IQueryable<T> query, int index, int pageSize);
        Task<T?> GetByIdAsync(object id);
        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();
    }
}
