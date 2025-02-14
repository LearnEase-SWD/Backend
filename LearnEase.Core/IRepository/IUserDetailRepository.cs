using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Infrastructure.IRepository
{
    public interface IUserDetailRepository
    {
        Task<UserDetail> getUserDetailByUserId(string userId);
        Task<UserDetail> getUserDetailById(string id);
        Task<UserDetail> CreateUserDetail(UserDetail userDetail);
        Task<UserDetail> UpdateUserDetail(UserDetail userDetail);
    }
}
