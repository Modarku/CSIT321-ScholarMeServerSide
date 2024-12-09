using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.Services.FlashcardInfo;

namespace ScholarMeServer.Controllers
{
    /**
     * POST(/flashcards/{userAccountId}/decks/{flashcardDeckId}/cards) - CreateFlashcard(int flashcardDeckId, FlashcardCreateDto flashcardDto);
     * GET(/flashcards/{userAccountId}/decks/{flashcardDeckId}/cards) - GetFlashcardsByDeckId(int flashcardDeckId);
     * PUT(/flashcards/{userAccountId}/decks/{flashcardDeckId}/cards/{flashcardId}) - UpdateFlashcard(int flashcardDeckId, FlashcardUpdateDto flashcardDto);
     * DELETE(/flashcards/{userAccountId}/decks/{flashcardDeckId}/cards/{flashcardId}) - DeleteFlashcard(int flashcardId);
     */
    [ApiController]
    [Route("api/flashcards/{userAccountId:int}/decks/{flashcardDeckId:int}/cards")]
    [Authorize]
    public class FlashcardsController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;

        public FlashcardsController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcard([FromRoute] int flashcardDeckId, FlashcardCreateDto flashcardDto)
        {
            var createdFlashcard = await _flashcardService.CreateFlashcard(flashcardDeckId, flashcardDto);
            return CreatedAtRoute("", new { id = createdFlashcard.Id }, createdFlashcard);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcardsByDeckId([FromRoute] int flashcardDeckId)
        {
            var flashcards = await _flashcardService.GetFlashcardsByDeckId(flashcardDeckId);
            return Ok(flashcards);
        }

        [HttpPut]
        [Route("{flashcardDeckId:int}")]
        public async Task<IActionResult> UpdateFlashcard([FromRoute] int flashcardDeckId, FlashcardUpdateDto flashcardDto)
        {
            var updatedFlashcard = await _flashcardService.UpdateFlashcard(flashcardDeckId, flashcardDto);
            return Ok(updatedFlashcard);
        }

        [HttpDelete]
        [Route("{flashcardDeckId:int}")]
        public async Task<IActionResult> DeleteFlashcard([FromRoute] int flashcard)
        {
            await _flashcardService.DeleteFlashcard(flashcard);
            return NoContent();
        }
    }
}
