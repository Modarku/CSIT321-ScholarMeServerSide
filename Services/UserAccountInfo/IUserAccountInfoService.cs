using RestTest.Models;
using ScholarMeServer.DTO.UserAccount;

namespace ScholarMeServer.Services.UserAccountInfo
{
    public interface IUserAccountInfoService
    {
        public Task<UserAccountReadOnlyDto> SignUpUser(UserAccountSignUpDto userAccountDto);
        public Task<UserAccountReadOnlyDto> SignInUser(UserAccountSignInDto userAccountDto);
        public Task<UserAccountReadOnlyDto> UpdateUserAccount(int userAccountId, UserAccountUpdateDto userAccountDto);
        public Task UpdateUserPassword(int userAccountId, UserAccountChangePasswordDto userAccountDto);
    }
}
