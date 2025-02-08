using System.Net.Http;
using System.Security.Claims;
using LearnEase_Api.dto.reponse;
using LearnEase_Api.dto.request;
using LearnEase_Api.Models.RedisCacheService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearnEase_Api.Models.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRedisCacheService _redisCacherService;
        private readonly IConfiguration _configuration;
        public AuthService(IHttpClientFactory httpClientFactory,IConfiguration configuration, IHttpContextAccessor httpContextAccessor,IRedisCacheService redisCacheService)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _redisCacherService = redisCacheService;
            _configuration = configuration;
        }

     

        public async Task<ApiResponse<DecodeTokenReponse>> GetTokenInfo(RequestToken request)
        {
            if (string.IsNullOrEmpty(request.IdToken))
            {
                return new ApiResponse<DecodeTokenReponse>
                {
                    Success = false,
                    Message = "Token is required"
                };
            }

            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={request.IdToken}");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<DecodeTokenReponse>
                    {
                        Success = true,
                        Data = new DecodeTokenReponse(json["email"]?.ToString(), json["exp"]?.ToString(), json["issued_to"]?.ToString())
                        {
                        }
                    };
                }
                else
                {
                    return new ApiResponse<DecodeTokenReponse>
                    {
                        Success = false,
                        Message = "Invalid token",
                        Error = json.ToString()
                    };
                }
            }
        }

       

        public async Task<ApiResponse<string>> RefreshToken(RequestRefreshToken request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Refresh Token is required"
                };
            }

            var values = new Dictionary<string, string>
            {
                { "client_id", _configuration["Authentication:Google:ClientId"] },
                { "client_secret",_configuration["Authentication:Google:ClientSecret"] },
                { "refresh_token", request.RefreshToken },
                { "grant_type", "refresh_token" }
            };

            var content = new FormUrlEncodedContent(values);
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Failed to refresh token"
                    };
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                return new ApiResponse<string>
                {
                    Success = true,
                    Data = responseData["access_token"]
                };
            }

        }

        public async Task<ApiResponse<bool>> VerifyAccessToken(RequestToken request)
        {
            if (string.IsNullOrEmpty(request.IdToken))
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Token is required"
                };
            }

            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={request.IdToken}");
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool>
                    {
                        Success = true,
                        Data = true
                    };
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Invalid token",
                        Error = json.ToString()
                    };
                }
            }


        }

        public async Task<ApiResponse<bool>> RevokeTokenAsync(RequestToken request)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var requestUri = $"https://accounts.google.com/o/oauth2/revoke?token={request.IdToken}";
                var response = await client.PostAsync(requestUri, null);

                return new ApiResponse<bool>
                {
                    Success = response.IsSuccessStatusCode,
                    Data = response.IsSuccessStatusCode,
                    Message = response.IsSuccessStatusCode ? "Token revoked successfully" : "Failed to revoke token"
                };
            }
        }

        public async Task<ApiResponse<bool>> Logout(RequestToken request)
        {
            var resultRevoke = await RevokeTokenAsync(request);

            if (!resultRevoke.Success)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Failed to revoke token from Google"
                };
            }

            await _redisCacherService.RemoveAsync(request.IdToken);

            return new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Logout successful"
            };
        }

    }
}
