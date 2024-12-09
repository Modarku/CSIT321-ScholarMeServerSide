using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.FlashcardChoice;
using ScholarMeServer.Services.FlashcardChoiceInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/flashcards/{userAccountId:int}/decks/{flashcardDeckId:int}/cards/{flashcardId:int}/choices")]
    [Authorize]
    public class FlashcardChoicesController : ControllerBase
    {
        private readonly IFlashcardChoiceService _flashcardChoiceService;

        public FlashcardChoicesController(IFlashcardChoiceService flashcardChoiceService)
        {
            _flashcardChoiceService = flashcardChoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcardChoice([FromRoute] int flashcardId, [FromBody] FlashcardChoiceCreateDto flashcardChoiceDto)
        {
            var createdFlashcardChoice = await _flashcardChoiceService.CreateFlashcardChoice(flashcardId, flashcardChoiceDto);
            return Ok(flashcardId);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcardChoicesByCardId([FromRoute] int flashcardId)
        {
            var flashcardChoices = await _flashcardChoiceService.GetFlashcardChoicesByCardId(flashcardId);
            return Ok(flashcardChoices);
        }

        [HttpGet]
        [Route("{flashcardChoiceId:int}", Name = "GetFlashcardChoiceById")]
        public async Task<IActionResult> GetFlashcardChoiceById([FromRoute] int flashcardChoiceId)
        {
            var flashcardChoice = await _flashcardChoiceService.GetFlashcardChoiceById(flashcardChoiceId);
            return Ok(flashcardChoice);
        }

        [HttpPut]
        [Route("{flashcardChoiceId:int}")]
        public async Task<IActionResult> UpdateFlashcardChoice([FromRoute] int flashcardChoiceId, [FromBody] FlashcardChoiceUpdateDto flashcardChoiceDto)
        {
            var updatedFlashcardChoice = await _flashcardChoiceService.UpdateFlashcardChoice(flashcardChoiceId, flashcardChoiceDto);
            return Ok(updatedFlashcardChoice);
        }

        [HttpDelete]
        [Route("{flashcardChoiceId:int}")]
        public async Task<IActionResult> DeleteFlashcardChoice([FromRoute] int flashcardChoiceId)
        {
            await _flashcardChoiceService.DeleteFlashcardChoice(flashcardChoiceId);
            return NoContent();
        }
    }
}
