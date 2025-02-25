using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearnEase_Api.LearnEase.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Task<ApiResponse<string>> GetGoogleLoginUrl()
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
        }

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
    }
}
