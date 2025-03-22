using LearnEase.Core.Models.Request;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/lessons")]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessons(int pageIndex = 1, int pageSize = 10)
    {
        var response = await _lessonService.GetLessonsAsync(pageIndex, pageSize);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        var response = await _lessonService.GetLessonByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] LessonCreationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _lessonService.CreateLessonAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] LessonCreationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _lessonService.UpdateLessonAsync(id, request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var response = await _lessonService.DeleteLessonAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }
}
