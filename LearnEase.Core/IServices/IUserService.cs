using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IUserService
    {
        Task<List<UserReponse>> getAllUser();
        Task<UserReponse> getUserReponseById(string id);
        Task<UserReponse> createNewUser(userCreationRequest request);
        Task<UserReponse> updateUserReponse(UserUpdateRequest request, string id);
        Task<UserReponse> deleteUserReponseById(string id);
        Task<UserReponse> findUserByEmail(string email);
    }
}
