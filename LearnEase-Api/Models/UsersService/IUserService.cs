using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;

namespace LearnEase_Api.Models.Users
{
    public interface IUserService
    {
        Task<List<UserReponse>> getAllUser();
        Task<UserReponse> getUserReponseById(string id);
        Task<UserReponse> createNewUser(userCreationRequest request);
        Task<UserReponse> updateUserReponse(UserUpdateRequest request,string id);
        Task<UserReponse> deleteUserReponseById(string id);
        Task<UserReponse> findUserByEmail(string email);
    }
}
