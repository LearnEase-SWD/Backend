using Google.Apis.Auth.OAuth2.Requests;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLessons()
    {
        var lessons = await _lessonService.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null) return NotFound();
        return Ok(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] Lesson lesson)
    {
        if (lesson == null) return BadRequest("Invalid data.");
        await _lessonService.CreateLessonAsync(lesson);
        return CreatedAtAction(nameof(GetLessonById), new { id = lesson.LessonID }, lesson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] Lesson lesson)
    {
        var result = await _lessonService.UpdateLessonAsync(id, lesson);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var result = await _lessonService.DeleteLessonAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}



