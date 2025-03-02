using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork
{
    private bool disposed = false;
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TRepo GetRepository<TRepo>() where TRepo : class
    {
        return _serviceProvider.GetRequiredService<TRepo>();
    }

    public void BeginTransaction() => _dbContext.Database.BeginTransaction();
    public void CommitTransaction() => _dbContext.Database.CommitTransaction();
    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    public void Rollback() => _dbContext.Database.RollbackTransaction();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _dbContext.Dispose();
        }
        disposed = true;
    }
}
