﻿using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Core.Entities;

[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var courses = await _courseService.GetCoursesAsync(pageIndex, pageSize);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound(new { message = "Course not found." });
        }
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        if (course == null)
        {
            return BadRequest(new { message = "Invalid course data." });
        }
        var newCourse = await _courseService.CreateCourseAsync(course);
        return CreatedAtAction(nameof(GetById), new { id = newCourse.Data }, newCourse);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Course course)
    {
        if (course == null || id != course.CourseID)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _courseService.DeleteCourseAsync(id);
        if (!deleted.Data)
        {
            return NotFound(new { message = "Course not found." });
        }
        return NoContent();
    }
}
