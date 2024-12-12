using RestTest.Models;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Repository.UserAccountInfo;
using ScholarMeServer.Utilities.Exceptions;
using System.Net;


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
            var user = await _userAccountInfoRepository.GetUserByUsername(userAccountDto.Username.ToLower());
            if (user != null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "User already exists");
            }

            var account = new UserAccount
            {
                Username = userAccountDto.Username.ToLower(),
                Email = userAccountDto.Email.ToLower(),
                Password = HashPassword(userAccountDto.Password),
                FirstName = userAccountDto.FirstName,
                LastName = userAccountDto.LastName,
                PhoneNumber = userAccountDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userAccountInfoRepository.CreateUserAccount(account);

            return new UserAccountReadOnlyDto()
            {
                Id = account.Id,
                Username = account.Username,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PhoneNumber = account.PhoneNumber,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };
        }

        public async Task<UserAccountReadOnlyDto> SignInUser(UserAccountSignInDto userAccountDto)
        {
            var user = await _userAccountInfoRepository.GetUserByUsername(userAccountDto.Username.ToLower());

            if (user == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "User not found");
            }

            if (!Verify(userAccountDto.Password, user.Password))
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "Incorrect password");
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
            var user = await _userAccountInfoRepository.GetUserById(userAccountId);

            if (user == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "User not found");
            }

            if (userAccountDto.Email != null)
            {
                user.Email = userAccountDto.Email;
            }
            if (userAccountDto.FirstName != null)
            {
                user.FirstName = userAccountDto.FirstName;
            }
            if (userAccountDto.LastName != null)
            {
                user.LastName = userAccountDto.LastName;
            }
            if (userAccountDto.PhoneNumber != null)
            {
                user.PhoneNumber = userAccountDto.PhoneNumber;
            }

            user.UpdatedAt = DateTime.UtcNow;

            await _userAccountInfoRepository.SaveUser(user);

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

        public async Task UpdateUserPassword(int userAccountId, UserAccountChangePasswordDto userAccountDto)
        {
            var user = await _userAccountInfoRepository.GetUserById(userAccountId);

            if (user == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "User not found");
            }

            if (!Verify(userAccountDto.OldPassword, user.Password))
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "Incorrect password");
            }

            user.Password = HashPassword(userAccountDto.NewPassword);

            await _userAccountInfoRepository.SaveUser(user);
        }
    }
}
