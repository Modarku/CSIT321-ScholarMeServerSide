using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.UserAccountInfo
{
    public class UserAccountInfoRepository : BaseRepository, IUserAccountInfoRepository
    {
        public UserAccountInfoRepository(ScholarMeDbContext scholarmeDbContext) : base(scholarmeDbContext) { }

        public async Task<UserAccount> CreateUserAccount(UserAccount userAccount)
        {
            _scholarmeDbContext.Set<UserAccount>().Add(userAccount);
            await _scholarmeDbContext.SaveChangesAsync();

            return userAccount;
        }

        public async Task<UserAccount?> GetUserByUsername(string username)
        {
            return await _scholarmeDbContext.Set<UserAccount>().SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserAccount?> GetUserById(int userAccountId)
        {
            return await _scholarmeDbContext.Set<UserAccount>().FindAsync(userAccountId);
        }

        public async Task SaveUser(UserAccount userAccount)
        {
            _scholarmeDbContext.Set<UserAccount>().Update(userAccount);
            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
