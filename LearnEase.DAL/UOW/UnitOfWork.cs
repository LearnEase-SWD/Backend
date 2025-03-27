using LearnEase.Repository;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.Repositories;
using LearnEase.Repository.UOW;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider; 
    private readonly Dictionary<Type, object> _repository = new();
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    // Lấy generic repository có CRUD
    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        if (!_repository.ContainsKey(typeof(T)))
        {
            _repository[typeof(T)] = new GenericRepository<T>(_dbContext);
        }
        return (IGenericRepository<T>)_repository[typeof(T)];
    }

    // Lấy repository có hàm riêng 
    public T GetCustomRepository<T>() where T : class
    {
        if (!_repository.TryGetValue(typeof(T), out var repository))
        {
            var implementationType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            repository = ActivatorUtilities.CreateInstance(_serviceProvider, implementationType);
            _repository[typeof(T)] = repository;
        }
        return (T)repository;
    }

    public async Task BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
    public async Task RollbackAsync() => await _dbContext.Database.RollbackTransactionAsync();
    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _dbContext.Dispose();
        }
        _disposed = true;
    }
}
