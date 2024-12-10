using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Services.UserAccountInfo;
using ScholarMeServer.Utilities;
using System.Security.Claims;

namespace ScholarMeServer.Controllers
{
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
        public async Task<IActionResult> SignUp([FromBody] UserAccountSignUpDto userAccountDto)
        {
            var user = await _userAccountInfoService.SignUpUser(userAccountDto);
            var token = _jwt.GenerateJwtToken(user);

            return Ok(new {user, token});
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoService.SignInUser(userAccountDto);
            var token = _jwt.GenerateJwtToken(user);

            return Ok(new { user, token });
        }

        [HttpPut("edit-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAccount([FromBody] UserAccountUpdateDto userAccountDto)
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userAccountInfoService.UpdateUserAccount(userAccountId, userAccountDto);
            return Ok(user);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserAccountChangePasswordDto userAccountDto)
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userAccountInfoService.UpdateUserPassword(userAccountId, userAccountDto);
            return NoContent();
        }
    }
}