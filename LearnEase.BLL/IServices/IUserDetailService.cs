using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IUserDetailService
    {
        Task<UserDetailResponse> getUserDetailByUserId(string userId);
        Task<UserDetailResponse> getUserDetailById(string id);
        Task<UserDetailResponse> CreateUserDetail(UserDetailRequest userDetail);
        Task<UserDetailResponse> UpdateUserDetail(UserDetailRequest userDetail);
    }
}
