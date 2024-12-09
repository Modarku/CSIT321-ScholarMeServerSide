using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.Services.FlashcardInfo;

namespace ScholarMeServer.Controllers
{
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
            return CreatedAtRoute("GetFlashcardById", new { id = createdFlashcard.Id }, createdFlashcard);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcardsByDeckId([FromRoute] int flashcardDeckId)
        {
            var flashcards = await _flashcardService.GetFlashcardsByDeckId(flashcardDeckId);
            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{flashcardId:int}", Name = "GetFlashcardById")]
        public async Task<IActionResult> GetFlashcardById([FromRoute] int flashcardId)
        {
            var flashcard = await _flashcardService.GetFlashcardById(flashcardId);
            return Ok(flashcard);
        }

        [HttpPut]
        [Route("{flashcardId:int}")]
        public async Task<IActionResult> UpdateFlashcard([FromRoute] int flashcardId, FlashcardUpdateDto flashcardDto)
        {
            var updatedFlashcard = await _flashcardService.UpdateFlashcard(flashcardId, flashcardDto);
            return Ok(updatedFlashcard);
        }

        [HttpDelete]
        [Route("{flashcardId:int}")]
        public async Task<IActionResult> DeleteFlashcard([FromRoute] int flashcardId)
        {
            await _flashcardService.DeleteFlashcard(flashcardId);
            return NoContent();
        }
    }
}
