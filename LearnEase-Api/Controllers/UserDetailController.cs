using LearnEase_Api.Dtos.request;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
    [Route("/api/userDetails")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailService _userDetailService;


        public UserDetailController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserDetail([FromBody] UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null)
            {
                return BadRequest("UserDetailRequest không hợp lệ.");
            }

            var result = await _userDetailService.CreateUserDetail(userDetailRequest);

            if (result == null)
            {
                return StatusCode(500, "Không thể tạo thông tin người dùng.");
            }

            return CreatedAtAction(nameof(GetUserDetailById), new { id = result.Id }, result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id không hợp lệ.");
            }

            var userDetailResponse = await _userDetailService.getUserDetailById(id);

            if (userDetailResponse == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            return Ok(userDetailResponse);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserDetailByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId không hợp lệ.");
            }

            var userDetailResponse = await _userDetailService.getUserDetailByUserId(userId);

            if (userDetailResponse == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            return Ok(userDetailResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDetail(string id, [FromBody] UserDetailRequest userDetailRequest)
        {
            if (userDetailRequest == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var updatedUserDetail = await _userDetailService.UpdateUserDetail(userDetailRequest);

            if (updatedUserDetail == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng để cập nhật.");
            }

            return Ok(updatedUserDetail);
        }
    }
}
