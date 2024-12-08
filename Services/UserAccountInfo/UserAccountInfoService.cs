using RestTest.Models;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Repository.UserAccountInfo;


namespace ScholarMeServer.Services.UserAccountInfo
{
    public class UserAccountInfoService : IUserAccountInfoService
    {
        private readonly IUserAccountInfoRepository _userAccountInfoRepository;

        public UserAccountInfoService(IUserAccountInfoRepository userAccountRepository)
        {
            _userAccountInfoRepository = userAccountRepository;
        }

        public async Task<UserAccountDto> SignUpUserAsync(UserAccountSignUpDto userAccountDto)
        {
            var existingUser = await _userAccountInfoRepository.GetUserByUsernameAsync(userAccountDto.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var userAccount = new UserAccount
            {
                Username = userAccountDto.Username,
                Email = userAccountDto.Email,
                Password = HashPassword(userAccountDto.Password),
                FirstName = userAccountDto.FirstName,
                LastName = userAccountDto.LastName,
                PhoneNumber = userAccountDto.PhoneNumber
            };

            await _userAccountInfoRepository.AddUserAsync(userAccount);
            
            return new UserAccountDto()
            {
                Id = userAccount.Id,
                Username = userAccount.Username,
                Email = userAccount.Email,
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                PhoneNumber = userAccount.PhoneNumber,
                CreatedAt = userAccount.CreatedAt,
                UpdatedAt = userAccount.UpdatedAt
            };
        }
        public async Task<UserAccountDto> SignInUserAsync(UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoRepository.GetUserByUsernameAsync(userAccountDto.Username);

            if (user == null || !Verify(userAccountDto.Password, user.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            return new UserAccountDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
