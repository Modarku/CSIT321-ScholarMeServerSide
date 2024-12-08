using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.Flashcard;
using ScholarMeServer.DTO.FlashcardSet;
using ScholarMeServer.Services.FlashcardSetInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashcardSetsController : ControllerBase
    {
        private readonly IFlashcardSetInfoService _flashcardSetInfoService;

        public FlashcardSetsController(IFlashcardSetInfoService flashcardSetInfoService )
        {
            _flashcardSetInfoService = flashcardSetInfoService;
        }

        [HttpGet]
        [Route("user/{userAccountId}")]
        public async Task<IActionResult> GetFlashcardSets(int userAccountId)
        {
            var flashcardSets = await _flashcardSetInfoService.GetFlashcardSets(userAccountId);
            return Ok(flashcardSets);
        }

        [HttpGet]
        [Route("{id}", Name = "GetFlashcardSetById")]
        public async Task<IActionResult> GetFlashcardSetById(int id)
        {
            var flashcardSet = await  _flashcardSetInfoService.GetFlashcardSetById(id);

            if (flashcardSet == null)
            {
                return NotFound($"Product with id {id} does not exist.");
            }

            return Ok(flashcardSet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcardSet(FlashcardSetCreateDto flashcardDto)
        {
            var createdFlashcardSet = await _flashcardSetInfoService.CreateFlashcardSet(flashcardDto);
            return CreatedAtRoute("GetFlashcardSetById", new { id = createdFlashcardSet.Id }, createdFlashcardSet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFlashcardSet(int id, FlashcardSetUpdateDto flashcardDto)
        {
            var updatedFlashcardSet = await _flashcardSetInfoService.UpdateFlashcardSet(id, flashcardDto);

            if (updatedFlashcardSet == null)
            {
                return NotFound($"Product with id {id} does not exist.");
            }

            return Ok(updatedFlashcardSet);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFlashcardSetById(int id)
        {
            await _flashcardSetInfoService.DeleteFlashcardSet(id);
            return NoContent();
        }
    }
}
