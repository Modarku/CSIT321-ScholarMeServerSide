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
        public Task<UserAccountReadOnlyDto> UpdateUserAccount(Guid userAccountId, UserAccountUpdateDto userAccountDto);
        public Task UpdateUserPassword(Guid userAccountId, UserAccountChangePasswordDto userAccountDto);
        public Task<UserAccountReadOnlyDto> GetUserById(Guid userId);
        public Task<RefreshTokenReadOnly> CreateRefreshToken(Guid userId, string token, DateTime expires);
        public Task<RefreshTokenReadOnly> UpdateRefreshToken(string oldToken, string newToken, DateTime expires);
        public Task UpdateUserAvatar(Guid userId, string avatarUrl);
    }
}
