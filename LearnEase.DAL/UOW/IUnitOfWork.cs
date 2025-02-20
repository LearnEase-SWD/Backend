using LearnEase.Repository.IRepository;

namespace LearnEase.Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        TRepo GetRepository<TRepo>() where TRepo : class;
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }

}
