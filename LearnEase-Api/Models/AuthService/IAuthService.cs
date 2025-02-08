using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;

namespace LearnEase_Api.Models.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<DecodeTokenReponse>> GetTokenInfo(RequestToken request);
        Task<ApiResponse<bool>> VerifyAccessToken(RequestToken request);
        Task<ApiResponse<String>> RefreshToken(RequestRefreshToken request);
        Task<ApiResponse<bool>> RevokeTokenAsync(RequestToken request);
        Task<ApiResponse<bool>> Logout(RequestToken request);
    }
}
