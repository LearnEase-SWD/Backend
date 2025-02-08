using LearnEase_Api.Entity;

namespace LearnEase_Api.Repository.UserDetailRepository
{
    public interface IUserDetailRepository
    {
        Task<UserDetail> getUserDetailByUserId(String userId);
        Task<UserDetail> getUserDetailById(String id);
        Task<UserDetail> CreateUserDetail(UserDetail userDetail);
        Task<UserDetail> UpdateUserDetail(UserDetail userDetail);
    }
}
