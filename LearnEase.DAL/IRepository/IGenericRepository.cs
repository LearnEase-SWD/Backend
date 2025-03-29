using System.Linq.Expressions;
using LearnEase.Core;

namespace LearnEase.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<BasePaginatedList<T>> GetPaggingAsync(IQueryable<T> query,
                                                            int index,
                                                            int pageSize,
                                                            List<Func<IQueryable<T>, IQueryable<T>>>? filterFuncs = null,
                                                            Func<IQueryable<T>, IQueryable<T>>? includeFunc = null);
		Task<T?> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate,
                          Func<IQueryable<T>, IQueryable<T>>? includeFunc = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate,
                           Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
