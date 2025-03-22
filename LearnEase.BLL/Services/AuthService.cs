using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRedisCacheService _redisCacherService;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration,IRedisCacheService redis)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _redisCacherService = redis;
        }

        /*public async Task<ApiResponse<UserResponse>> SignupWithGoogle(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
            {
                return new ApiResponse<UserResponse> { Success = false, Message = "ID Token is required." };
            }

            // Xác thực ID Token với Google
            using var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={idToken}");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (!response.IsSuccessStatusCode || json["email"] == null)
            {
                return new ApiResponse<UserResponse> { Success = false, Message = "Invalid ID Token." };
            }

            string email = json["email"]?.ToString();
            string name = json["name"]?.ToString();
            string picture = json["picture"]?.ToString();

            // Kiểm tra xem user đã tồn tại chưa
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                // Tạo mới user
                user = new User
                {
                    Email = email,
                    Name = name,
                    ProfilePicture = picture,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            return new ApiResponse<UserResponse>
            {
                Success = true,
                Data = new UserResponse
                {
                    Email = user.Email,
                    Name = user.Name,
                    ProfilePicture = user.ProfilePicture
                }
            };
        }
*/
        /*public Task<ApiResponse<string>> GetGoogleLoginUrl()
        {
            var clientId = _configuration["Authentication:Google:ClientId"];
            var redirectUri = _configuration["Authentication:Google:RedirectUri"];
            var scope = "openid email profile";
            var state = "random-state-value"; 

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri))
            {
                return Task.FromResult(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Google OAuth configuration is missing."
                });
            }

            var url = $"https://accounts.google.com/o/oauth2/auth" +
                      $"?client_id={clientId}" +
                      $"&redirect_uri={redirectUri}" +
                      $"&response_type=code" +
                      $"&scope={scope}" +
                      $"&state={state}" +
                      $"&access_type=offline";

            return Task.FromResult(new ApiResponse<string>
            {
                Success = true,
                Data = url
            });
        }*/

        public async Task<ApiResponse<string>> ExchangeCodeForToken(string code)
        {
            var tokenUrl = "https://oauth2.googleapis.com/token";
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientSecret = _configuration["Authentication:Google:ClientSecret"];
            var redirectUri = _configuration["Authentication:Google:RedirectUri"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(redirectUri))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Google OAuth configuration is missing."
                };
            }

            var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            var content = new FormUrlEncodedContent(values);
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(tokenUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Failed to exchange code for token.",
                    Error = errorResponse
                };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            if (json != null && json.TryGetValue("id_token", out var idToken))
            {
                return new ApiResponse<string>
                {
                    Success = true,
                    Data = idToken
                };
            }

            return new ApiResponse<string>
            {
                Success = false,
                Message = "Failed to retrieve ID token."
            };
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
