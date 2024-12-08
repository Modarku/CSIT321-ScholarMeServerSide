using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Services.UserAccountInfo;

namespace ScholarMeServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountInfoService _userAccountInfoService;

        public UserAccountsController(IUserAccountInfoService userAccountInfoService)
        {
            _userAccountInfoService = userAccountInfoService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserAccountSignUpDto userAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userAccountInfoService.SignUpUserAsync(userAccountDto);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserAccountSignInDto userAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userAccountInfoService.SignInUserAsync(userAccountDto);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
