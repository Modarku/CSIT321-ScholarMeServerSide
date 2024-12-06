using RestTest.DTO;
using RestTest.Models;
using RestTest.Repository.IRepository;
using RestTest.Services.IServices;

namespace RestTest.Services
{
    public class UserAccountService : IUserAccountService
    {
        public readonly IUserAccountRepository _repository;

        public UserAccountService(IUserAccountRepository repository)
        {
            _repository = repository;
        }

        public List<UserAccountDTO> GetAllUsers()
        {
            List<UserAccountDTO> users = new List<UserAccountDTO>();
            var rUsers = _repository.GetAllUsers();

            foreach (UserAccount rUser in rUsers)
            {
                users.Add(new UserAccountDTO()
                {
                    Username = rUser.Username,
                    Email = rUser.Email,
                    Password = rUser.Password,
                    FirstName = rUser.FirstName,
                    LastName = rUser.LastName,
                    PhoneNumber = rUser.PhoneNumber,
                    ProfilePic = rUser.ProfilePic,
                    Role = rUser.Role,
                    Status = rUser.Status,
                    DateAdded = rUser.DateAdded,
                    DateUpdated = rUser.DateUpdated
                });
            }

            return users;
        }

        public UserAccountDTO? GetUserById(int id) 
        {
            var rUser = _repository.GetById(id);

            if (rUser == null)
                return null;
            

            return new UserAccountDTO()
            {
                Username = rUser.Username,
                Email = rUser.Email,
                Password = rUser.Password,
                FirstName = rUser.FirstName,
                LastName = rUser.LastName,
                PhoneNumber = rUser.PhoneNumber,
                ProfilePic = rUser.ProfilePic,
                Role = rUser.Role,
                Status = rUser.Status,
                DateAdded = rUser.DateAdded,
                DateUpdated = rUser.DateUpdated
            };


        }
    }
}
