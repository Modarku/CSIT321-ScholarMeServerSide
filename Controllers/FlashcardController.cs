using Microsoft.AspNetCore.Mvc;
using RestTest.Services;
using RestTest.Services.IServices;

namespace RestTest.Controllers
{
    [ApiController]
    [Route("api/flashcard")]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;
        private readonly IFlashcardChoiceService _flashcardChoiceService;

        public FlashcardController(IFlashcardService flashcardService, IFlashcardChoiceService flashcardChoiceService)
        {
            _flashcardService = flashcardService;
            _flashcardChoiceService = flashcardChoiceService;
        }

        [HttpGet]
        public IActionResult GetAllFlashcards()
        {
            var flashcards = _flashcardService.GetAllFlashcards();

            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{fcid}/choice")]
        public IActionResult GetFlashcardChoiceById(int fcid)
        {
            var flashcard = _flashcardChoiceService.GetAllFlashcardChoicesByFlashcardID(fcid);

            return Ok(flashcard);
        }
    }
}
