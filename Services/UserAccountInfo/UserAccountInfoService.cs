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

        public async Task<UserAccountReadOnlyDto> SignUpUser(UserAccountSignUpDto userAccountDto)
        {
            var existingUser = await _userAccountInfoRepository.GetUserByUsername(userAccountDto.Username);
            if (existingUser != null)
            {
                // TODO:
                throw new NotImplementedException("Validation logic not yet implemented!");
            }

            var userAccount = new UserAccount
            {
                Username = userAccountDto.Username,
                Email = userAccountDto.Email,
                Password = HashPassword(userAccountDto.Password),
                FirstName = userAccountDto.FirstName,
                LastName = userAccountDto.LastName,
                PhoneNumber = userAccountDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userAccountInfoRepository.CreateUserAccount(userAccount);

            return new UserAccountReadOnlyDto()
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

        public async Task<UserAccountReadOnlyDto> SignInUser(UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoRepository.GetUserByUsername(userAccountDto.Username);

            if (user == null)
            {
                // TODO:
                throw new NotImplementedException("User Not Found: Validation logic not yet implemented!");
            }

            if (!Verify(userAccountDto.Password, user.Password))
            {
                // TODO:
                throw new NotImplementedException("Incorrect Password: Validation logic not yet implemented!");
            }

            return new UserAccountReadOnlyDto()
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

        public async Task<UserAccountReadOnlyDto> UpdateUserAccount(int userAccountId, UserAccountUpdateDto userAccountDto)
        {
            var existingUser = await _userAccountInfoRepository.GetUserById(userAccountId);

            if (existingUser == null)
            {
                // TODO:
                throw new NotImplementedException("User Not Found: Validation logic not yet implemented!");
            }

            if (!string.IsNullOrEmpty(userAccountDto.Email))
            {
                existingUser.Email = userAccountDto.Email;
            }
            if (!string.IsNullOrEmpty(userAccountDto.FirstName))
            {
                existingUser.FirstName = userAccountDto.FirstName;
            }
            if (!string.IsNullOrEmpty(userAccountDto.LastName))
            {
                existingUser.LastName = userAccountDto.LastName;
            }
            if (!string.IsNullOrEmpty(userAccountDto.PhoneNumber))
            {
                existingUser.PhoneNumber = userAccountDto.PhoneNumber;
            }

            existingUser.UpdatedAt = DateTime.UtcNow;

            await _userAccountInfoRepository.SaveUser(existingUser);

            return new UserAccountReadOnlyDto()
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                PhoneNumber = existingUser.PhoneNumber,
                CreatedAt = existingUser.CreatedAt,
                UpdatedAt = existingUser.UpdatedAt
            };
        }

        public async Task UpdateUserPassword(int userAccountId, UserAccountChangePasswordDto userAccountDto)
        {
            var existingUser = await _userAccountInfoRepository.GetUserById(userAccountId);

            if (existingUser == null)
            {
                // TODO:
                throw new NotImplementedException("User Not Found: Validation logic not yet implemented!");
            }

            if (!Verify(userAccountDto.OldPassword, existingUser.Password))
            {
                // TODO:
                throw new NotImplementedException("Incorrect Password: Validation logic not yet implemented!");
            }

            existingUser.Password = HashPassword(userAccountDto.NewPassword);

            await _userAccountInfoRepository.SaveUser(existingUser);
        }
    }
}
