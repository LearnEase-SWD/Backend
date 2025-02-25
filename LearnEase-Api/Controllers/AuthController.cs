using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("login-url")]
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
    }
}
