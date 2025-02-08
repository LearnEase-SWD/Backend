using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;
using LearnEase_Api.Entity;

namespace LearnEase_Api.Models.UserDetailService
{
    public interface IUserDetailService
    {
        Task<UserDetailResponse> getUserDetailByUserId(String userId);
        Task<UserDetailResponse> getUserDetailById(String id);
        Task<UserDetailResponse> CreateUserDetail(UserDetailRequest userDetail);
        Task<UserDetailResponse> UpdateUserDetail(UserDetailRequest userDetail);
    }
}
