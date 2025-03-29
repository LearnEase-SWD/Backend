using System.Linq.Expressions;
using LearnEase.Core;
using LearnEase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
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

		public async Task<T?> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
		{
			IQueryable<T> query = _dbSet;

			// Include thêm entity liên quan
			if (includeFunc != null)
			{
				query = includeFunc(query);
			}

			// Lấy tên property khóa chính động
			var keyName = _context.Model
								  .FindEntityType(typeof(T))
								  ?.FindPrimaryKey()
								  ?.Properties
								  .FirstOrDefault()
								  ?.Name;

			if (keyName == null)
				throw new InvalidOperationException($"Entity '{typeof(T).Name}' không xác định được khóa chính.");

			// Sử dụng tên property động để truy vấn
			var entity = await query.FirstOrDefaultAsync(e => EF.Property<object>(e, keyName) == id);

			return entity;
		}

		public async Task<BasePaginatedList<T>> GetPaggingAsync(IQueryable<T> query,
	                                                        int index,
	                                                        int pageSize,
	                                                        List<Func<IQueryable<T>, IQueryable<T>>>? filterFuncs = null, 
	                                                        Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
		{
			query = query.AsNoTracking();

			// Include thêm entity liên quan
			if (includeFunc != null)
			{
				query = includeFunc(query);
			}

			// Thêm filter để lọc 
			if (filterFuncs != null)
			{
				foreach (var filter in filterFuncs)
				{
					query = filter(query);
				}
			}

			int count = await query.CountAsync();
			IReadOnlyCollection<T> items = await query.Skip((index - 1) * pageSize)
													  .Take(pageSize)
													  .ToListAsync();

			return new BasePaginatedList<T>(items, count, index, pageSize);
		}
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

    }
}

