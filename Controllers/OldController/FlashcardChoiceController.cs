using Microsoft.AspNetCore.Mvc;
using RestTest.Services;
using RestTest.Services.IServices;

namespace RestTest.Controllers
{
    [ApiController]
    [Route("api/choice")]
    public class FlashcardChoiceController : ControllerBase
    {
        private readonly IFlashcardChoiceService _flashcardChoiceService;

        public FlashcardChoiceController(IFlashcardChoiceService flashcardChoiceService)
        {
            _flashcardChoiceService = flashcardChoiceService;
        }

        //[HttpGet]
        //public IActionResult GetAllFlashcardChoices()
        //{
        //    var flashcards = _flashcardChoiceService.GetAllFlashcardChoices();

        //    return Ok(flashcards);
        //}

        [HttpGet]
        [Route("{fcid}/choice/{cid}")]
        public IActionResult GetFlashcardChoiceById(int cid)
        {
            var flashcard = _flashcardChoiceService.GetFlashcardChoiceById(cid);

            return Ok(flashcard);
        }
    }
}
