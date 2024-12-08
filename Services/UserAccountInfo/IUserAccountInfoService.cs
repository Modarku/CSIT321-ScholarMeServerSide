using RestTest.Models;
using ScholarMeServer.DTO.UserAccount;

namespace ScholarMeServer.Services.UserAccountInfo
{
    public interface IUserAccountInfoService
    {
        Task<UserAccountDto> SignUpUserAsync(UserAccountSignUpDto userAccountDto);
        Task<UserAccountDto> SignInUserAsync(UserAccountSignInDto userAccountDto);
    }
}
