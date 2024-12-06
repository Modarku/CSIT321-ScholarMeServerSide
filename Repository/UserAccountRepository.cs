using RestTest.Models;
using RestTest.Repository.IRepository;

namespace RestTest.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        public List<UserAccount> GetAllUsers()
        {
            return DB.Users;
        }
        public UserAccount? GetById(int id)
        {
            return DB.Users.SingleOrDefault(x => x.UserId == id);
        }

    }
}
