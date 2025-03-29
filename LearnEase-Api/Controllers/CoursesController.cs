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
    [HttpGet("{courseid}")]
    public async Task<IActionResult> GetByIdAsync(Guid courseid)
    {
        var course = await _courseService.GetCourseByIdAsync(courseid);
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


    [HttpPost("{purchase}/purchase")]
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
        // Kiểm tra userId có tồn tại không
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID is required." });
        }

        try
        {
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
   
    [HttpDelete("{idcourse}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id);
        if (!deleted.Data)
        {
            return NotFound(new { message = "Course not found." });
        }
        return NoContent();
    }
    [HttpGet("search")]
    public async Task<IActionResult> SearchCourses(string title, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var response = await _courseService.SearchCoursesByTitleAsync(title, pageIndex, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }
        catch (Exception ex)
        {
            // Log the exception properly!
            Console.WriteLine($"Error in SearchCourses: {ex}");
            return StatusCode(500, new BaseResponse<IEnumerable<CourseResponse>>(
                StatusCodeHelper.ServerError,
                "ERROR",
                null,
                "Lỗi hệ thống khi tìm kiếm khóa học."
            ));
        }
    }
}
