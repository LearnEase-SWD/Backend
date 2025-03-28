using Microsoft.AspNetCore.Mvc;
using LearnEase.Repository.IRepository;
using LearnEase.Core.Entities;
using LearnEase.Service.IServices;

[Route("api/flashcards")]
[ApiController]
public class FlashcardsController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;

    public FlashcardsController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var flashcards = await _flashcardService.GetFlashcardsAsync(pageIndex, pageSize);
        return Ok(flashcards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var flashcard = await _flashcardService.GetFlashcardByIdAsync(id);
        if (flashcard == null)
        {
            return NotFound(new { message = "Flashcard not found." });
        }
        return Ok(flashcard);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Flashcard flashcard)
    {
        if (flashcard == null)
        {
            return BadRequest(new { message = "Invalid flashcard data." });
        }
        await _flashcardService.CreateFlashcardAsync(flashcard);
        return CreatedAtAction(nameof(GetById), new { id = flashcard.FlashcardID }, flashcard);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Flashcard flashcard)
    {
        if (flashcard == null || id != flashcard.FlashcardID)
        {
            return BadRequest(new { message = "Invalid flashcard data or mismatched ID." });
        }
        var result = await _flashcardService.UpdateFlashcardAsync(id, flashcard);
        if (!result.Data)
        {
            return NotFound(new { message = "Flashcard not found." });
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _flashcardService.DeleteFlashcardAsync(id);
        if (!result.Data)
        {
            return NotFound(new { message = "Flashcard not found." });
        }
        return NoContent();
    }
}
