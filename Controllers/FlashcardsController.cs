using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.Services.FlashcardInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashcardsController : ControllerBase
    {
        private readonly IFlashcardInfoService _flashcardInfoService;

        public FlashcardsController(IFlashcardInfoService flashcardInfoService)
        {
            _flashcardInfoService = flashcardInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashcards()
        {
            var flashcards = await _flashcardInfoService.GetFlashcardsAsync();
            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{id}", Name = "GetFlashcardById")]
        public async Task<IActionResult> GetFlashcardById(int id)
        {
            var flashcard = await _flashcardInfoService.GetFlashcardByIdAsync(id);
            if (flashcard == null)
            {
                return NotFound($"Flashcard with id {id} does not exists.");
            }
            return Ok(flashcard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcard([FromBody] FlashcardCreateDto flashcardDto)
        {
            var createdFlashcard = await _flashcardInfoService.CreateFlashcardAsync(flashcardDto);

            return CreatedAtAction(nameof(GetFlashcardById), new { id = createdFlashcard.Id }, createdFlashcard);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFlashcard(int id, [FromBody] FlashcardCreateDto flashcardDto)
        {
            var updatedFlashcard = await _flashcardInfoService.UpdateFlashcardAsync(id, flashcardDto);
            if (updatedFlashcard == null)
            {
                return NotFound($"Flashcard with id {id} does not exists");
            }

            return Ok(updatedFlashcard);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFlashcard(int id)
        {
            await _flashcardInfoService.DeleteFlashcardAsync(id);

            return NoContent();
        }
    }
}
