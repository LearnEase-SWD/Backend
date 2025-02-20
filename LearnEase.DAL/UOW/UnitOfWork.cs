using LearnEase_Api.LearnEase.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LearnEase.Repository.UOW
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IServiceProvider _serviceProvider;

        public TRepo GetRepository<TRepo>() where TRepo : class
        {
            return _serviceProvider.GetRequiredService<TRepo>();
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
