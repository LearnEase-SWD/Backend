using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IUserService
    {
        Task<UserReponse> GetUserReponseById(string id);
        Task<UserReponse> CreateNewUser(UserCreationRequest request);
        Task<UserReponse> UpdateUserReponse(UserUpdateRequest request, string id);
        Task<UserReponse> DeleteUserReponseById(string id);
        Task<UserReponse> FindUserByEmail(string email);
    }
}
