using LearnEase.Core;
using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> Entities => _context.Set<T>();

        public async Task CreateAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public Task UpdateAsync(T obj)
        {
            return Task.FromResult(_dbSet.Update(obj));
        }

        public async Task DeleteAsync(object id)
        {
            T entity = await _dbSet.FindAsync(id) ?? throw new Exception();
            _dbSet.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<BasePaginatedList<T>> GetPagging(IQueryable<T> query, int index, int pageSize)
        {
            query = query.AsNoTracking();
            int count = await query.CountAsync();
            IReadOnlyCollection<T> items = await query.Skip((pageSize - 1) * pageSize).Take(pageSize).ToListAsync();
            return new BasePaginatedList<T>(items, count, index, pageSize);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
