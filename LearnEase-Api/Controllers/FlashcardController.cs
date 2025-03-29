
﻿using Microsoft.AspNetCore.Mvc;
using LearnEase.Service.IServices;
﻿using Microsoft.AspNetCore.Authorization;
using LearnEase.Core.Models.Request;


[ApiController]
[Route("api/flashcards")]
[AllowAnonymous]
public class FlashcardsController : ControllerBase
{
	private readonly IFlashcardService _flashcardService;

	public FlashcardsController(IFlashcardService flashcardService)
	{
		_flashcardService = flashcardService;
	}

	[HttpGet]
	public async Task<IActionResult> GetFlashcards(int pageIndex = 1, int pageSize = 10)
	{
		var response = await _flashcardService.GetFlashcardsAsync(pageIndex, pageSize);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetFlashcardById(Guid id)
	{
		var response = await _flashcardService.GetFlashcardByIdAsync(id);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateFlashcard([FromBody] FlashcardRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var response = await _flashcardService.CreateFlashcardAsync(request);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateFlashcard(Guid id, [FromBody] FlashcardRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var response = await _flashcardService.UpdateFlashcardAsync(id, request);
		return StatusCode((int)response.StatusCode, response);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteFlashcard(Guid id)
	{
		var response = await _flashcardService.DeleteFlashcardAsync(id);
		return StatusCode((int)response.StatusCode, response);
	}

}
