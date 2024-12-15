using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.Services.FlashcardInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
    [Authorize]
    public class FlashcardsController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;

        public FlashcardsController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        [HttpPost("decks/{flashcardDeckId:Guid}/cards")]
        public async Task<IActionResult> CreateFlashcard([FromRoute] Guid flashcardDeckId, [FromBody] FlashcardCreateDto flashcardDto)
        {
            var createdFlashcard = await _flashcardService.CreateFlashcard(flashcardDeckId, flashcardDto);
            return CreatedAtRoute("GetFlashcardById", new { flashcardId = createdFlashcard.Id }, createdFlashcard);
        }

        [HttpGet("decks/{flashcardDeckId:Guid}/cards")]
        public async Task<IActionResult> GetFlashcardsByDeckId([FromRoute] Guid flashcardDeckId, [FromQuery] bool includeChoices = false)
        {
            var flashcards = await _flashcardService.GetFlashcardsByDeckId(flashcardDeckId, includeChoices);
            return Ok(flashcards);
        }

        [HttpGet("cards/{flashcardId:Guid}", Name = "GetFlashcardById")]
        public async Task<IActionResult> GetFlashcardById([FromRoute] Guid flashcardId, [FromQuery] bool includeChoices = false)
        {
            var flashcard = await _flashcardService.GetFlashcardById(flashcardId, includeChoices);
            return Ok(flashcard);
        }

        [HttpPut("cards/{flashcardId:Guid}")]
        public async Task<IActionResult> UpdateFlashcard([FromRoute] Guid flashcardId, [FromBody] FlashcardUpdateDto flashcardDto)
        {
            var updatedFlashcard = await _flashcardService.UpdateFlashcard(flashcardId, flashcardDto);
            return Ok(updatedFlashcard);
        }

        [HttpDelete("cards/{flashcardId:Guid}")]
        public async Task<IActionResult> DeleteFlashcard([FromRoute] Guid flashcardId)
        {
            await _flashcardService.DeleteFlashcard(flashcardId);
            return NoContent();
        }
    }
}
