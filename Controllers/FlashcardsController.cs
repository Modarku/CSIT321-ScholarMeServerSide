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

        [HttpPost("decks/{flashcardDeckId:int}/cards")]
        public async Task<IActionResult> CreateFlashcard([FromRoute] int flashcardDeckId, [FromBody] FlashcardCreateDto flashcardDto)
        {
            var createdFlashcard = await _flashcardService.CreateFlashcard(flashcardDeckId, flashcardDto);
            return CreatedAtRoute("GetFlashcardById", new { flashcardId = createdFlashcard.Id }, createdFlashcard);
        }

        [HttpGet("decks/{flashcardDeckId:int}/cards")]
        public async Task<IActionResult> GetFlashcardsByDeckId([FromRoute] int flashcardDeckId)
        {
            var flashcards = await _flashcardService.GetFlashcardsByDeckId(flashcardDeckId);
            return Ok(flashcards);
        }

        [HttpGet("cards/{flashcardId:int}", Name = "GetFlashcardById")]
        public async Task<IActionResult> GetFlashcardById([FromRoute] int flashcardId)
        {
            var flashcard = await _flashcardService.GetFlashcardById(flashcardId);
            return Ok(flashcard);
        }

        [HttpPut("cards/{flashcardId:int}")]
        public async Task<IActionResult> UpdateFlashcard([FromRoute] int flashcardId, [FromBody] FlashcardUpdateDto flashcardDto)
        {
            var updatedFlashcard = await _flashcardService.UpdateFlashcard(flashcardId, flashcardDto);
            return Ok(updatedFlashcard);
        }

        [HttpDelete("cards/{flashcardId:int}")]
        public async Task<IActionResult> DeleteFlashcard([FromRoute] int flashcardId)
        {
            await _flashcardService.DeleteFlashcard(flashcardId);
            return NoContent();
        }
    }
}
