using System.Security.Claims;
using Google.Apis.Auth.OAuth2.Requests;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Core.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IRedisCacheService _redisCacheService;

    public AuthController(IAuthService authService,IUserService userService,IRedisCacheService redisCacheService)
    {
        _authService = authService;
        _userService = userService;
        _redisCacheService = redisCacheService;
    }

    /*[HttpPost("sign-up")]
    public async Task<IActionResult> Signup([FromBody] TokenRequest request)
    {
        if (string.IsNullOrEmpty(request.IdToken))
            return BadRequest("ID Token is required.");

        var result = await _authService.SignupWithGoogle(request.IdToken);
        if (!result.Success)
            return StatusCode(500, result.Message);

        return Ok(result.Data);
    }*/

   /* [HttpGet("login-url")]
    public async Task<IActionResult> GetGoogleLoginUrl()
    {
        var result = await _authService.GetGoogleLoginUrl();
        if (!result.Success)
            return StatusCode(500, result.Message);

        return Ok(new { url = result.Data });
    }

    [HttpGet("callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string code)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Missing authorization code.");

        var result = await _authService.ExchangeCodeForToken(code);
        if (!result.Success)
            return StatusCode(500, result.Message);

        return Ok(new { id_token = result.Data });
    }*/



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
            var findUserEmail = await _userService.FindUserByEmail(userEmail);
            if (findUserEmail == null)
            {
                await _userService.CreateNewUser(new userCreationRequest(userName, userEmail,null));
            }
        }

        CacheRequest cacheRequest = new CacheRequest(accessToken, userEmail, 60);
        await _redisCacheService.SetAsync(cacheRequest.key, cacheRequest.value, TimeSpan.FromMinutes(cacheRequest.time));

        return Ok(new { AccessToken = accessToken, UserEmail = userEmail, UserName = userName });
    }


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
