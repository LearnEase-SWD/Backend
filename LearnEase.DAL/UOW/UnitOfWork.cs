using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.Repository;
using LearnEase.Repository.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider; 
    private readonly Dictionary<Type, object> _repositoryCache = new();
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        if (!_repositoryCache.ContainsKey(typeof(T)))
        {
            _repositoryCache[typeof(T)] = new GenericRepository<T>(_dbContext);
        }
        return (IGenericRepository<T>)_repositoryCache[typeof(T)];
    }

    public T GetCustomRepository<T>() where T : class
    {
        if (!_repositoryCache.TryGetValue(typeof(T), out var repository))
        {
            var implementationType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (implementationType == null)
            {
                throw new InvalidOperationException($"Không tìm thấy repository phù hợp cho {typeof(T).Name}");
            }

            repository = ActivatorUtilities.CreateInstance(_serviceProvider, implementationType);
            _repositoryCache[typeof(T)] = repository;
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
