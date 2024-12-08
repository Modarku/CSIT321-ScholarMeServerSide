using RestTest.Models;

namespace ScholarMeServer.Repository.UserAccountInfo
{
    public interface IUserAccountInfoRepository
    {
        Task<UserAccount?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(UserAccount userAccount);
    }
}
