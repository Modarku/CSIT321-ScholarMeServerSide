using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Services.UserAccountInfo;
using ScholarMeServer.Utilities;

namespace ScholarMeServer.Controllers
{
    // TODO: Restrict access of UpdateUserAccount and UpdateUserPassword routes to their owner.

    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountInfoService _userAccountInfoService;
        private readonly JwtService _jwt;

        public UserAccountsController(IUserAccountInfoService userAccountInfoService, JwtService jwt)
        {
            _userAccountInfoService = userAccountInfoService;
            _jwt = jwt;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserAccountSignUpDto userAccountDto)
        {
            var user = await _userAccountInfoService.SignUpUser(userAccountDto);
            return Ok(user);
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoService.SignInUser(userAccountDto);
            var token = _jwt.GenerateJwtToken(user);

            return Ok(new { user, token });
        }

        [HttpPut]
        [Route("{userAccountId:int}/edit-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAccount(int userAccountId, UserAccountUpdateDto userAccountDto)
        {
            var user = await _userAccountInfoService.UpdateUserAccount(userAccountId, userAccountDto);
            return Ok(user);
        }

        [HttpPut]
        [Route("{userAccountId:int}/change-password")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPassword(int userAccountId, UserAccountChangePasswordDto userAccountDto)
        {

            await _userAccountInfoService.UpdateUserPassword(userAccountId, userAccountDto);
            return NoContent();
        }
    }
}
