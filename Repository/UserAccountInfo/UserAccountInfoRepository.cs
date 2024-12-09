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

        public async Task<UserAccount?> UpdateUserAccount(UserAccount userAccount)
        {
            var existingUser = await _scholarmeDbContext.Set<UserAccount>().FindAsync(userAccount.Id);

            if (existingUser != null)
            {
                if (!string.IsNullOrEmpty(userAccount.Email))
                {
                    existingUser.Email = userAccount.Email;
                }
                if (!string.IsNullOrEmpty(userAccount.FirstName))
                {
                    existingUser.FirstName = userAccount.FirstName;
                }
                if (!string.IsNullOrEmpty(userAccount.LastName))
                {
                    existingUser.LastName = userAccount.LastName;
                }
                if (!string.IsNullOrEmpty(userAccount.PhoneNumber))
                {
                    existingUser.PhoneNumber = userAccount.PhoneNumber;
                }
                existingUser.UpdatedAt = DateTime.UtcNow;
                await this.SaveUser(existingUser);
            }
            return existingUser;
        }

        public async Task UpdateUserPassword(UserAccount userAccount)
        {
            var existingUser = await _scholarmeDbContext.Set<UserAccount>().FindAsync(userAccount.Id);

            if (existingUser != null)
            {
                existingUser.Password = userAccount.Password;
                existingUser.UpdatedAt = DateTime.UtcNow;
                await this.SaveUser(existingUser);
            }
        }

        public async Task SaveUser(UserAccount userAccount)
        {
            _scholarmeDbContext.Set<UserAccount>().Update(userAccount);
            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
