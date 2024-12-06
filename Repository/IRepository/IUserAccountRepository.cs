using RestTest.Models;

namespace RestTest.Repository.IRepository
{
    public interface IUserAccountRepository
    {
        public List<UserAccount> GetAllUsers();
        public UserAccount? GetById(int id);
    }
}
