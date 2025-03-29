using LearnEase.Core.Models.Request;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
    [Route("api/theory-lessons")]
    [ApiController]
    [AllowAnonymous]
    public class TheoryLessonController : ControllerBase
    {
        private readonly ITheoryLessonService _theoryLessonService;

        public TheoryLessonController(ITheoryLessonService theoryLessonService)
        {
            _theoryLessonService = theoryLessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTheoryLessons(int pageIndex = 1, int pageSize = 10)
        {
            var response = await _theoryLessonService.GetTheoryLessonsAsync(pageIndex, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheoryLessonById(Guid id)
        {
            var response = await _theoryLessonService.GetTheoryLessonByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTheoryLesson([FromBody] TheoryLessonCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _theoryLessonService.CreateTheoryLessonAsync(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTheoryLesson(Guid id, [FromBody] TheoryLessonCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _theoryLessonService.UpdateTheoryLessonAsync(id, request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheoryLesson(Guid id)
        {
            var response = await _theoryLessonService.DeleteTheoryLessonAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
