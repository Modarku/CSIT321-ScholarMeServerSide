using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.FlashcardDeck;
using ScholarMeServer.Services.FlashcardDeckInfo;
using System.Security.Claims;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/flashcards/decks")]
    [Authorize]
    public class FlashcardDecksController : ControllerBase
    {
        private readonly IFlashcardDeckService _flashcardDeckService;

        public FlashcardDecksController(IFlashcardDeckService flashcardDeckService)
        {
            _flashcardDeckService = flashcardDeckService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcardDeck(FlashcardDeckCreateDto flashcardDeckDto)
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var createdFlashcardDeck = await _flashcardDeckService.CreateFlashcardDeck(userAccountId, flashcardDeckDto);
            return CreatedAtRoute("GetFlashcardDeckById", new { id = createdFlashcardDeck.Id }, createdFlashcardDeck);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFlashcardDecks()
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var flashcardDecks = await _flashcardDeckService.GetFlashcardDecksByUserId(userAccountId);
            return Ok(flashcardDecks);
        }

        [HttpGet("{flashcardDeckId:int}", Name = "GetFlashcardDeckById")]
        public async Task<IActionResult> GetFlashcardDeckById([FromRoute] int flashcardDeckId)
        {
            var flashcardDeck = await _flashcardDeckService.GetFlashcardDeckById(flashcardDeckId);
            return Ok(flashcardDeck);
        }

        [HttpPut("{flashcardDeckId:int}")]
        public async Task<IActionResult> UpdateFlashcardDeck([FromRoute] int flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var updatedFlashcardDeck = await _flashcardDeckService.UpdateFlashcardDeck(flashcardDeckId, flashcardDeckDto);
            return Ok(updatedFlashcardDeck);
        }

        [HttpDelete("{flashcardDeckId:int}")]
        public async Task<IActionResult> DeleteFlashcardDeck([FromRoute] int flashcardDeckId)
        {
            await _flashcardDeckService.DeleteFlashcardDeck(flashcardDeckId);
            return NoContent();
        }
    }
}
