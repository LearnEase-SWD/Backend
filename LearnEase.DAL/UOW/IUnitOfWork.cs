using LearnEase.Repository.IRepository;

namespace LearnEase.Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        T GetCustomRepository<T>() where T : class;
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackAsync();
    }

}
