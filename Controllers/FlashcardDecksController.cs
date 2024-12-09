using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.FlashcardDeck;
using ScholarMeServer.Services.FlashcardDeckInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/flashcards/{userAccountId:int}/decks")]
    [Authorize]
    public class FlashcardDecksController : ControllerBase
    {
        private readonly IFlashcardDeckService _flashcardDeckService;

        public FlashcardDecksController(IFlashcardDeckService flashcardDeckService)
        {
            _flashcardDeckService = flashcardDeckService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcardDeck([FromRoute] int userAccountId, FlashcardDeckCreateDto flashcardDeckDto)
        {
            var createdFlashcardDeck = await _flashcardDeckService.CreateFlashcardDeck(userAccountId, flashcardDeckDto);
            return CreatedAtRoute("", new { id = createdFlashcardDeck.Id }, createdFlashcardDeck);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcardDecksByUserId([FromRoute] int userAccountId)
        {
            var flashcardDecks = await _flashcardDeckService.GetFlashcardDecksByUserId(userAccountId);
            return Ok(flashcardDecks);
        }

        [HttpGet]
        [Route("{flashcardDeckId:int")]
        public async Task<IActionResult> GetFlashcardDeckById([FromRoute] int flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckService.GetFlashcardDeckById(flashcardDeckId);
            return Ok(flashcardDeck);
        }

        [HttpPut]
        [Route("{flashcardDeckId:int}")]
        public async Task<IActionResult> UpdateFlashcardDeck([FromRoute] int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var updatedFlashcardDeck = await _flashcardDeckService.UpdateFlashcardDeck(flashcardDeckId, flashcardDeckDto);
            return Ok(updatedFlashcardDeck);
        }

        [HttpDelete]
        [Route("{flashcardDeckId:int}")]
        public async Task<IActionResult> DeleteFlashcardDeck([FromRoute] int flashcardDeckId)
        {
            await _flashcardDeckService.DeleteFlashcardDeck(flashcardDeckId);
            return NoContent();
        }
    }
}
