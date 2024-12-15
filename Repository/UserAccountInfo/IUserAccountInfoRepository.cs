using RestTest.Models;
using ScholarMeServer.Models;

namespace ScholarMeServer.Repository.UserAccountInfo
{
    public interface IUserAccountInfoRepository
    {
        public Task<UserAccount> CreateUserAccount(UserAccount userAccount);

        public Task<UserAccount?> GetUserByUsername(string username);

        public Task<UserAccount?> GetUserById(Guid userAccountId);

        public Task SaveUser(UserAccount userAccount);

        public Task<RefreshToken?> GetRefreshToken(string token);

        public Task SaveRefreshToken(RefreshToken refreshToken);

    }
}
