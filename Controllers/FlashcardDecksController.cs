using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.FlashcardDeck;
using ScholarMeServer.Services.FlashcardDeckInfo;

namespace ScholarMeServer.Controllers
{
    /**
     * POST(/flashcards/{userAccountId}/decks) - CreateFlashcardDeck(int userAccountId, FlashcardDeckCreateDto flashcardDeckDto);  
     * GET(/flashcards/{userAccountId}/decks) - GetFlashcardDecks(int userAccountId);  
     * PUT(/flashcards/{userAccountId}/decks/{flashcardDeckId}) - UpdateFlashcardDeck(int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto); 
     * DELETE(/flashcards/{userAccountId}/decks/{flashcardDeckId}) - DeleteFlashcardDeck(int flashcardDeckId);
     */
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
        public async Task<IActionResult> GetFlashcardDecks([FromRoute] int userAccountId)
        {
            var flashcardDecks = await _flashcardDeckService.GetFlashcardDecks(userAccountId);
            return Ok(flashcardDecks);
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
