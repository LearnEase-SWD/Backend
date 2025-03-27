using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;

namespace LearnEase_Api.LearnEase.Infrastructure.IRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> FindByName(string name);
        Task<User> FindByEmail(string email);
    }
}
