using Microsoft.AspNetCore.Mvc;
using RestTest.DTO;
using RestTest.Models;
using RestTest.Services;
using RestTest.Services.IServices;

namespace RestTest.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IFlashcardSetService _flashcardSetService;
        public UserAccountController(IUserAccountService userAccountService, IFlashcardSetService flashcardSetService)
        {
            _userAccountService = userAccountService;
            _flashcardSetService = flashcardSetService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userAccountService.GetAllUsers();
            
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id) 
        {
            var user = _userAccountService.GetUserById(id);

            if (user == null)
                return NotFound($"User with id {id} not found.");

            return Ok(user);
        }

        [HttpGet]
        [Route("{uid}/set")]
        public IActionResult GetAllFlashcardSetsByUserId(int uid)
        {
            var flashcardSets = _flashcardSetService.GetAllFlashcardSetsByUserId(uid);

            return Ok(flashcardSets);
        }

        [HttpGet]
        [Route("{uid}/set/{fcsid}")]
        public IActionResult GetFlashcardSetById(int fcsid, int uid)
        {
            var flashcardSet = _flashcardSetService.GetFlashcardSetById(fcsid, uid);

            return Ok(flashcardSet);
        }
    }
}
