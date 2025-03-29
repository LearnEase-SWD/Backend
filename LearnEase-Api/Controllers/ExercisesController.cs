using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/exercises")]
[AllowAnonymous]
public class ExercisesController : ControllerBase
{
	private readonly IExerciseService _exerciseService;

	public ExercisesController(IExerciseService exerciseService)
	{
		_exerciseService = exerciseService;
	}

	[HttpGet]
	public async Task<IActionResult> GetExercises(int pageIndex = 1, int pageSize = 10)
	{
		var response = await _exerciseService.GetExercisesAsync(pageIndex, pageSize);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetExerciseById(Guid id)
	{
		var response = await _exerciseService.GetExerciseByIdAsync(id);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateExercise([FromBody] ExerciseRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var response = await _exerciseService.CreateExerciseAsync(request);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] ExerciseRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var response = await _exerciseService.UpdateExerciseAsync(id, request);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteExercise(Guid id)
	{
		var response = await _exerciseService.DeleteExerciseAsync(id);
		return StatusCode((int)response.StatusCode, response);
	}

    [HttpPost("mark-exercise-completed")]
    public async Task<IActionResult> MarkExerciseCompleted(string userId, Guid exerciseId)
    {
        var response = await _exerciseService.MarkExerciseAsCompletedAsync(userId, exerciseId);

        if (response.StatusCode == StatusCodeHelper.OK && response.Code == "SUCCESS")
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

}
