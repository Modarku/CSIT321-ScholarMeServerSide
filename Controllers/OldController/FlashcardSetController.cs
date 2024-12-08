using Microsoft.AspNetCore.Mvc;
using RestTest.Models;
using RestTest.Services;
using RestTest.Services.IServices;

namespace RestTest.Controllers
{
    [ApiController]
    [Route("api/flashcard_set")]
    public class FlashcardSetController : ControllerBase
    {
        private readonly IFlashcardSetService _flashcardSetService;
        private readonly IFlashcardService _flashcardService;

        public FlashcardSetController(IFlashcardSetService flashcardSetService, IFlashcardService flashcardService)
        {
            _flashcardSetService = flashcardSetService;
            _flashcardService = flashcardService;
        }

        [HttpGet]
        public IActionResult GetAllFlashcardSets()
        {
            var flashcardSets = _flashcardSetService.GetAllFlashcardSets();

            return Ok(flashcardSets);
        }

        [HttpGet]
        [Route("{fcsid}")]
        public IActionResult GetAllFlashcardsBySetId(int fcsid)
        {
            var flashcards = _flashcardService.GetAllFlashcardsBySetId(fcsid);

            return Ok(flashcards);
        }

        [HttpGet]
        [Route("{fcsid}/flashcard/{fcid}")]
        public IActionResult GetFlashcardById(int fcsid, int fcid)
        {
            var flashcard = _flashcardService.GetFlashcardById(fcsid, fcid);

            return Ok(flashcard);
        }
    }
}
