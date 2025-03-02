using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.LearnEase.Core.IServices
{
        public interface IAuthService
    {
        Task<ApiResponse<string>> GetGoogleLoginUrl();
        Task<ApiResponse<string>> ExchangeCodeForToken(string code);
        
    }
    }

