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

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var courses = await _courseService.GetCoursesAsync(pageIndex, pageSize);
        return Ok(courses);
    }

	/*[HttpPost("{courseId}/purchase")]
    [AllowAnonymous]  // Không yêu cầu xác thực
    public async Task<IActionResult> PurchaseCourse(Guid courseId, [FromQuery] string id)
    {
        // Kiểm tra id có tồn tại không
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest(new { message = "User ID is required." });
        }

        // Gọi Service để thực hiện mua khóa học
        var purchaseResult = await _courseService.PurchaseCourseAsync(courseId, id);

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
