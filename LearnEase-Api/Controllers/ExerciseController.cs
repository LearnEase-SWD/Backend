using Google.Apis.Auth.OAuth2.Requests;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExercises()
    {
        var exercises = await _exerciseService.GetAllExercisesAsync();
        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExerciseById(Guid id)
    {
        var exercise = await _exerciseService.GetExerciseByIdAsync(id);
        if (exercise == null) return NotFound();
        return Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
    {
        if (exercise == null) return BadRequest("Invalid data.");
        await _exerciseService.CreateExerciseAsync(exercise);
        return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.ExerciseID }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] Exercise exercise)
    {
        var result = await _exerciseService.UpdateExerciseAsync(id, exercise);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(Guid id)
    {
        var result = await _exerciseService.DeleteExerciseAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}


