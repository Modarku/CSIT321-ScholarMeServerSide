using Microsoft.EntityFrameworkCore;
using RestTest;
using RestTest.Models;
using ScholarMeServer.Models;

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

        public async Task<UserAccount?> GetUserById(Guid userAccountId)
        {
            return await _scholarmeDbContext.Set<UserAccount>().FindAsync(userAccountId);
        }

        public async Task SaveUser(UserAccount userAccount)
        {
            _scholarmeDbContext.Set<UserAccount>().Update(userAccount);
            await _scholarmeDbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> CreateRefreshToken(RefreshToken refreshToken)
        {
            _scholarmeDbContext.Set<RefreshToken>().Add(refreshToken);
            await _scholarmeDbContext.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await _scholarmeDbContext.Set<RefreshToken>().SingleOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            var existingToken = await _scholarmeDbContext.Set<RefreshToken>()
                .SingleOrDefaultAsync(rt => rt.Id == refreshToken.Id);

            if (existingToken == null)
            {
                _scholarmeDbContext.Set<RefreshToken>().Add(refreshToken);
            }
            else
            {
                _scholarmeDbContext.Entry(existingToken).CurrentValues.SetValues(refreshToken);
            }

            await _scholarmeDbContext.SaveChangesAsync();
        }
    }
}
