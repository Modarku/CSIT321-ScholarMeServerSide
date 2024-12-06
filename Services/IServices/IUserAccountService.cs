using RestTest.DTO;

namespace RestTest.Services.IServices
{
    public interface IUserAccountService
    {
        public List<UserAccountDTO> GetAllUsers();
        public UserAccountDTO? GetUserById(int id);
    }
}
