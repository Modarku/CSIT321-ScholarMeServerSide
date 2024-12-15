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
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var createdFlashcardDeck = await _flashcardDeckService.CreateFlashcardDeck(userAccountId, flashcardDeckDto);
            return CreatedAtRoute("GetFlashcardDeckById", new { flashcardDeckId = createdFlashcardDeck.Id }, createdFlashcardDeck);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFlashcardDecks()
        {
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var flashcardDecks = await _flashcardDeckService.GetFlashcardDecksByUserId(userAccountId);
            return Ok(flashcardDecks);
        }

        [HttpGet("{flashcardDeckId:Guid}", Name = "GetFlashcardDeckById")]
        public async Task<IActionResult> GetFlashcardDeckById([FromRoute] Guid flashcardDeckId, [FromQuery] bool includeFlashcards = false)
        {
            var flashcardDeck = await _flashcardDeckService.GetFlashcardDeckById(flashcardDeckId, includeFlashcards);
            return Ok(flashcardDeck);
        }

        [HttpPut("{flashcardDeckId:Guid}")]
        public async Task<IActionResult> UpdateFlashcardDeck([FromRoute] Guid flashcardDeckId, FlashcardDeckUpdateDto flashcardDeckDto)
        {
            var updatedFlashcardDeck = await _flashcardDeckService.UpdateFlashcardDeck(flashcardDeckId, flashcardDeckDto);
            return Ok(updatedFlashcardDeck);
        }

        [HttpDelete("{flashcardDeckId:Guid}")]
        public async Task<IActionResult> DeleteFlashcardDeck([FromRoute] Guid flashcardDeckId)
        {
            await _flashcardDeckService.DeleteFlashcardDeck(flashcardDeckId);
            return NoContent();
        }
    }
}
