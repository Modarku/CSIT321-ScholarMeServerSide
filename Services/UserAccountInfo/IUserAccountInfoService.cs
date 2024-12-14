using RestTest.Models;
using ScholarMeServer.DTO;
using ScholarMeServer.DTO.RefreshTokenRequest;
using ScholarMeServer.DTO.UserAccount;

namespace ScholarMeServer.Services.UserAccountInfo
{
    public interface IUserAccountInfoService
    {
        public Task<UserAccountReadOnlyDto> SignUpUser(UserAccountSignUpDto userAccountDto);
        public Task<UserAccountReadOnlyDto> SignInUser(UserAccountSignInDto userAccountDto);
        public Task<UserAccountReadOnlyDto> UpdateUserAccount(int userAccountId, UserAccountUpdateDto userAccountDto);
        public Task UpdateUserPassword(int userAccountId, UserAccountChangePasswordDto userAccountDto);
        public Task<UserAccountReadOnlyDto> GetUserById(int userId);
        public Task<RefreshTokenReadOnly> CreateRefreshToken(int userId, string token, DateTime expires);
        public Task UpdateRefreshToken(int userId, string oldToken, string newToken, DateTime expires);
    }
}
