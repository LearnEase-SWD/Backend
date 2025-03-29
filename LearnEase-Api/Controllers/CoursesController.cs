using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Core.Entities;
using LearnEase.Core.Models.Request;
using Microsoft.AspNetCore.Authorization;
using LearnEase.Core.Base;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Reponse;
using System.Security.Claims;

[Route("api/courses")]
[ApiController]
[AllowAnonymous]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound(new { message = "Course not found." });
        }
        return Ok(course);

        }
            [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var courses = await _courseService.GetCoursesAsync(pageIndex, pageSize);
        return Ok(courses);
        //mua khoa hoc
    }


    [HttpPost("{courseId}/purchase")]
    [AllowAnonymous]  // Không yêu cầu xác thực
    public async Task<IActionResult> PurchaseCourse(Guid courseId, [FromQuery] string userid)

    {
        // Kiểm tra id có tồn tại không
        if (string.IsNullOrEmpty(userid))
        {
            return BadRequest(new { message = "User ID is required." });
        }

        // Gọi Service để thực hiện mua khóa học
        var purchaseResult = await _courseService.PurchaseCourseAsync(courseId, userid);

        if (!purchaseResult.Data)
        {
            return StatusCode((int)purchaseResult.StatusCode, new { message = purchaseResult.Message });
        }

        return StatusCode((int)purchaseResult.StatusCode, purchaseResult);
    }*/

	[HttpGet("{id}")]
	public async Task<IActionResult> GetCourseByIdAsync(Guid id)
	{
		if (id == Guid.Empty)
			return BadRequest(new { message = "Invalid Course ID." });

		var course = await _courseService.GetCourseByIdAsync(id);

		if (course.Data == null)
			return NotFound(new { message = "Course not found." });

		return Ok(course);
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] CourseRequest course)
	{
		if (course == null)
			return BadRequest(new { message = "Invalid course data." });

		var newCourse = await _courseService.CreateCourseAsync(course);

		// Phản hồi thành công với dữ liệu vừa tạo
		return Ok(newCourse);
	}


	[HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CourseRequest course)
    {
        if (course == null)
        {
            return BadRequest(new { message = "Invalid course data or mismatched ID." });
        }
        var updated = await _courseService.UpdateCourseAsync(id, course);
        if (!updated.Data)
        {
            return NotFound(new { message = "Course not found." });
        }
        return NoContent();
    }
    [HttpGet("users/{userId}/courses")]
    [AllowAnonymous] // Chỉ người dùng đã đăng nhập mới xem được thông tin của mình
    public async Task<IActionResult> GetUserCoursesWithProgress(string userId)
    {
        try
        {
            // **Quan trọng:** Ở đây, bạn cần đảm bảo rằng userId được lấy từ xác thực (Authorization)
            // và KHÔNG được truyền trực tiếp từ client (để tránh giả mạo)
            string authenticatedUserId = GetCurrentUserId(); // Lấy userId từ token (hoặc từ session)

            if (authenticatedUserId != userId)
            {
                return Forbid("Bạn không có quyền xem thông tin khóa học của người dùng khác.");
            }

            var response = await _courseService.GetUserCoursesWithProgressAsync(userId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Ghi log lỗi (thay Console.WriteLine bằng ILogger)
            Console.WriteLine($"Error in GetUserCoursesWithProgress: {ex}");
            return StatusCode(500, new BaseResponse<IEnumerable<UserCourseResponse>>(
                StatusCodeHelper.ServerError,
                "ERROR",
                null,
                "Lỗi hệ thống khi lấy thông tin khóa học của người dùng."
            ));
        }
    }
    private string GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("Không tìm thấy User ID.");
        }
        return userId;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id);
        if (!deleted.Data)
        {
            return NotFound(new { message = "Course not found." });
        }
        return NoContent();
    }
}
