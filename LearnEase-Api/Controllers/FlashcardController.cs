using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FlashcardController : ControllerBase
{
  
    private readonly IFlashcardService _flashcardService;

    public FlashcardController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlashcardsAsync(int pageIndex, int pageSize)
    {
        var flashcards = await _flashcardService.GetFlashcardsAsync(pageIndex, pageSize);
        return Ok(flashcards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFlashcardById(Guid id)
    {
        var flashcard = await _flashcardService.GetFlashcardByIdAsync(id);
        if (flashcard == null) return NotFound();
        return Ok(flashcard);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlashcard([FromBody] Flashcard flashcard)
    {
        if (flashcard == null) return BadRequest("Invalid data.");
        await _flashcardService.CreateFlashcardAsync(flashcard);
        return CreatedAtAction(nameof(GetFlashcardById), new { id = flashcard.FlashcardID }, flashcard);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlashcard(Guid id, [FromBody] Flashcard flashcard)
    {
        var result = await _flashcardService.UpdateFlashcardAsync(id, flashcard);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlashcard(Guid id)
    {
        var result = await _flashcardService.DeleteFlashcardAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}


