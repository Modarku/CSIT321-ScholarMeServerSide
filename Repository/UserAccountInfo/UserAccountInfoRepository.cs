using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;

namespace ScholarMeServer.Repository.UserAccountInfo
{
    public class UserAccountInfoRepository : IUserAccountInfoRepository
    {
        private readonly ScholarMeDbContext _scholarmeDbContext;

        public UserAccountInfoRepository(ScholarMeDbContext scholarmeDbContext)
        {
            _scholarmeDbContext = scholarmeDbContext;
        }

        public async Task<UserAccount?> GetUserByUsernameAsync(string username)
        {
            return await _scholarmeDbContext.Set<UserAccount>().SingleOrDefaultAsync(u => u.Username == username);
        }
        public async Task AddUserAsync(UserAccount userAccount)
        {
            await _scholarmeDbContext.Set<UserAccount>().AddAsync(userAccount);
            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
