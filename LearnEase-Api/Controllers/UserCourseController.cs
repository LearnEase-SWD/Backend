using LearnEase.Core.IServices;
using LearnEase.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Core.Enum;

namespace LearnEase.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserCourseController : ControllerBase
	{
		private readonly IUserCourseService _userCourseService;

		public UserCourseController(IUserCourseService userCourseService)
		{
			_userCourseService = userCourseService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
		{
			var result = await _userCourseService.GetUserCoursesAsync(pageIndex, pageSize);
			if (result.StatusCode == StatusCodeHelper.OK)
				return Ok(result);

			return StatusCode((int)result.StatusCode, result);
		}

		[HttpGet("{userCourseId}")]
		public async Task<IActionResult> GetById(Guid userCourseId)
		{
			var result = await _userCourseService.GetUserCourseByIdAsync(userCourseId);
			if (result.StatusCode == StatusCodeHelper.OK)
				return Ok(result);

			return StatusCode((int)result.StatusCode, result);
		}
        [HttpGet("getByCourse/{courseId}")]
        public async Task<IActionResult> GetByCourseId(Guid courseId,string userId)
        {
            var result = await _userCourseService.GetUserCourseByIdCourseAsync(courseId, userId);
            if (result.StatusCode == StatusCodeHelper.OK)
                return Ok(result);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("getByListCourse/{userId}")]
        public async Task<IActionResult> GetListCourseByUserId(string userId)
        {
            var result = await _userCourseService.GetUserCourseUserIDAsync(userId);
            if (result.StatusCode == StatusCodeHelper.OK)
                return Ok(result);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
		public async Task<IActionResult> Create([FromBody] UserCourseRequest userCourseRequest)
		{
			if (!ModelState.IsValid)
				return BadRequest("Dữ liệu không hợp lệ.");

			var result = await _userCourseService.CreateUserCourseAsync(userCourseRequest);
			if (result.StatusCode == StatusCodeHelper.OK)
				return Ok(result);

			return StatusCode((int)result.StatusCode, result);
		}

		[HttpPut("{userCourseId}")]
		public async Task<IActionResult> Update(Guid userCourseId, [FromBody] UserCourseRequest userCourseRequest)
		{
			if (!ModelState.IsValid)
				return BadRequest("Dữ liệu không hợp lệ.");

			var result = await _userCourseService.UpdateUserCourseAsync(userCourseId, userCourseRequest);
			if (result.StatusCode == StatusCodeHelper.OK)
				return Ok(result);

			return StatusCode((int)result.StatusCode, result);
		}

		[HttpDelete("{userCourseId}")]
		public async Task<IActionResult> Delete(Guid userCourseId)
		{
			var result = await _userCourseService.DeleteUserCourseAsync(userCourseId);
			if (result.StatusCode == StatusCodeHelper.OK)
				return Ok(result);

			return StatusCode((int)result.StatusCode, result);
		}
	}
}
