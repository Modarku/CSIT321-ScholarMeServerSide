using RestTest.Models;

namespace ScholarMeServer.Repository.UserAccountInfo
{
    public interface IUserAccountInfoRepository
    {
        public Task<UserAccount> CreateUserAccount(UserAccount userAccount);

        public Task<UserAccount?> GetUserByUsername(string username);

        public Task<UserAccount?> GetUserById(int userAccountId);

        public Task SaveUser(UserAccount userAccount);
    }
}
