using LearnEase.Core.Entities;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/exercises")]
[ApiController]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var exercises = await _exerciseService.GetExercisesAsync(pageIndex, pageSize);
        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var exercise = await _exerciseService.GetExerciseByIdAsync(id);
        if (exercise == null)
        {
            return NotFound(new { message = "Exercise not found." });
        }
        return Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Exercise exercise)
    {
        if (exercise == null)
        {
            return BadRequest(new { message = "Invalid exercise data." });
        }
        await _exerciseService.CreateExerciseAsync(exercise);
        return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseID }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Exercise exercise)
    {
        if (exercise == null || id != exercise.ExerciseID)
        {
            return BadRequest(new { message = "Invalid exercise data or mismatched ID." });
        }
        var result = await _exerciseService.UpdateExerciseAsync(id, exercise);
        if (!result.Data)
        {
            return NotFound(new { message = "Exercise not found." });
        }
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _exerciseService.DeleteExerciseAsync(id);
        if (!result.Data)
        {
            return NotFound(new { message = "Exercise not found." });
        }
        return NoContent();
    }
}
