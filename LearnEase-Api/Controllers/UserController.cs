using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase.Core.Models.Request;

namespace LearnEase_Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
	[AllowAnonymous]
	public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public UserController(IUserService userService, HttpClient httpClient)
        {
            _userService = userService;
            _httpClient = httpClient;
        }

		[HttpGet]
		public async Task<IActionResult> GetUserAsync(int pageIndex = 1, int pageSize = 10)
		{
			var response = await _userService.GetUserAsync(pageIndex, pageSize);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserReponseById(id);
            if (result == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] UserCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid user data." });
            }

            var result = await _userService.CreateNewUser(request);
            if (result == null)
            {
                return BadRequest(new { message = "Failed to create user. Email might already exist." });
            }

            return CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid update data." });
            }

            var result = await _userService.UpdateUserReponse(request, id);
            if (result == null)
            {
                return BadRequest(new { message = "Failed to update user. Email might already exist." });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserReponseById(id);
            if (result == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return NoContent();
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Email cannot be null or empty." });
            }

            var result = await _userService.FindUserByEmail(email);
            if (result == null)
            {
                return NotFound(new { message = $"User with email '{email}' not found." });
            }

            return Ok(result);
        }
    }
}
