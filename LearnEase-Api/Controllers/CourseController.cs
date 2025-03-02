using Google.Apis.Auth.OAuth2.Requests;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        var newCourse = await _courseService.CreateCourseAsync(course);
        return CreatedAtAction(nameof(GetCourseById), new { id = newCourse.CourseID }, newCourse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] Course course)
    {
        var updated = await _courseService.UpdateCourseAsync(id, course);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}


