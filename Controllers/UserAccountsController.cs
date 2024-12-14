using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScholarMeServer.DTO;
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
            var accessToken = _jwt.GenerateJwtToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            // Update user's refresh token
            await _userAccountInfoService.CreateRefreshToken(user.Id, refreshToken, DateTime.UtcNow.AddDays(30));

            return Ok(new { user, accessToken, refreshToken });
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoService.SignInUser(userAccountDto);
            var accessToken = _jwt.GenerateJwtToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            // Update user's refresh token
            await _userAccountInfoService.CreateRefreshToken(user.Id, refreshToken, DateTime.UtcNow.AddDays(30));

            return Ok(new { user, accessToken, refreshToken });
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

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCreateDto refreshTokenDto)
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userAccountInfoService.GetUserById(userAccountId);
            
            var accessToken = _jwt.GenerateJwtToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            await _userAccountInfoService.UpdateRefreshToken(userAccountId, refreshTokenDto.Token, refreshToken, DateTime.UtcNow.AddDays(30));

            return Ok(new { user, accessToken, refreshToken });
        }
    }
}