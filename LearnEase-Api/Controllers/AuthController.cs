using LearnEase_Api.dto.request;
using LearnEase_Api.Models.AuthService;
using LearnEase_Api.Models.RedisCacheService;
using LearnEase_Api.Models.Users;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[AllowAnonymous]
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly HttpClient _httpClient;
    private readonly IUserService _userService;
    private readonly IRedisCacheService _redisCacheService;

    public AuthController(IAuthService authService, HttpClient httpClient, IUserService userService,
        IRedisCacheService redisCacheService)
    {
        _authService = authService;
        _httpClient = httpClient;
        _userService = userService;
        _redisCacheService = redisCacheService;
    }

    // Chỉ cần sử dụng AllowAnonymous ở cấp phương thức nếu muốn
    [HttpGet("login")]
    public IActionResult Login()
    {
        var redirectUri = "http://localhost:5121/api/auth/callback";
        var properties = new AuthenticationProperties { RedirectUri = redirectUri };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback()
    {
        var info = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (info?.Principal == null)
        {
            return Unauthorized();
        }

        var accessToken = info.Properties.GetTokenValue("access_token");
        var userEmail = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var userName = info.Principal.Identity?.Name;

        if (userEmail != null)
        {
            var findUserEmail = await _userService.findUserByEmail(userEmail);
            if (findUserEmail == null)
            {
                await _userService.createNewUser(new userCreationRequest(userName, userEmail));
            }
        }

        CacheRequest cacheRequest = new CacheRequest(accessToken, userEmail, 60);
        await _redisCacheService.SetAsync(cacheRequest.key, cacheRequest.value, TimeSpan.FromMinutes(cacheRequest.time));

        return Ok(new { AccessToken = accessToken, UserEmail = userEmail, UserName = userName });
    }

    // Các phương thức không cần xác thực cũng có thể sử dụng [AllowAnonymous]
    [AllowAnonymous]
    [HttpPost("verify-access-token")]
    public async Task<IActionResult> VerifyAccessToken([FromBody] RequestToken request)
    {
        var response = await _authService.VerifyAccessToken(request);
        if (response.Success)
        {
            return Ok(response);
        }

        return Unauthorized(response.Message);
    }

    [AllowAnonymous]
    [HttpPost("token-info")]
    public async Task<IActionResult> GetTokenInfo([FromBody] RequestToken request)
    {
        var response = await _authService.GetTokenInfo(request);
        if (response.Success)
        {
            return Ok(response);
        }

        return Unauthorized(response.Message);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RequestRefreshToken request)
    {
        var response = await _authService.RefreshToken(request);
        if (response.Success)
        {
            return Ok(response);
        }

        return Unauthorized(response.Message);
    }

    [AllowAnonymous]
    [HttpPost("/revokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody] RequestToken request)
    {
        var response = await _authService.RevokeTokenAsync(request);
        if (response.Success)
        {
            return Ok(response);
        }
        return Unauthorized(response.Message);
    }

    [AllowAnonymous]
    [HttpPost("/logout")]
    public async Task<IActionResult> Logout([FromBody] RequestToken request)
    {
        var response = await _authService.Logout(request);
        if (response.Success)
        {
            return Ok(response);
        }
        return Unauthorized(response.Message);
    }
}
