using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IAuthService
    {
        Task<ApiResponse<DecodeTokenReponse>> GetTokenInfo(RequestToken request);
        Task<ApiResponse<bool>> VerifyAccessToken(RequestToken request);
        Task<ApiResponse<string>> RefreshToken(RequestRefreshToken request);
        Task<ApiResponse<bool>> RevokeTokenAsync(RequestToken request);
        Task<ApiResponse<bool>> Logout(RequestToken request);
    }
}
