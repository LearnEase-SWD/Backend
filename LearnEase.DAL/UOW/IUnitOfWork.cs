using LearnEase.Repository.IRepository;

namespace LearnEase.Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        // Lấy generic repo
        IGenericRepository<T> GetRepository<T>() where T : class;
        // Lấy repo có hàm riêng 
        T GetCustomRepository<T>() where T : class;
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackAsync();
    }

}
