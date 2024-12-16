using RestTest.Models;
using ScholarMeServer.DTO;
using ScholarMeServer.DTO.File;
using ScholarMeServer.DTO.RefreshTokenRequest;
using ScholarMeServer.DTO.UserAccount;
using ScholarMeServer.Models;
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
                AvatarPath = null,
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
                AvatarPath = user.AvatarPath,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<UserAccountReadOnlyDto> UpdateUserAccount(Guid userAccountId, UserAccountUpdateDto userAccountDto)
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
            if (userAccountDto.avatar != null)
            {
                List<string> validExtensions = new List<string>() { ".jpg", ".jpeg", ".png", "gif" };
                IFormFile file = userAccountDto.avatar;

                string extension = Path.GetExtension(file.FileName);

                if (!validExtensions.Contains(extension))
                {
                    throw new HttpResponseException(400);
                }

                long size = file.Length;

                if (size > 5 * 1024 * 1024)
                {
                    throw new HttpResponseException(400);
                }

                string dir = Path.Combine(Directory.GetCurrentDirectory());
                string path = Path.Combine("Media", "Avatar");
                string fileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(path, fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.Combine(dir, path));

                // Delete the old avatar if it exists
                if (!string.IsNullOrEmpty(user.AvatarPath))
                {
                    string oldFilePath = Path.Combine(dir, user.AvatarPath);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                using FileStream stream = new FileStream(Path.Combine(dir, filePath), FileMode.Create);
                file.CopyTo(stream);

                user.AvatarPath = filePath.Replace("\\", "/");
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
                AvatarPath = user.AvatarPath,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task UpdateUserPassword(Guid userAccountId, UserAccountChangePasswordDto userAccountDto)
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

            user.UpdatedAt = DateTime.UtcNow;

            await _userAccountInfoRepository.SaveUser(user);
        }

        public async Task<UserAccountReadOnlyDto> GetUserById(Guid userId)
        {
            var user = await _userAccountInfoRepository.GetUserById(userId);

            if (user == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "User not found");
            }

            return new UserAccountReadOnlyDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                AvatarPath = user.AvatarPath,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<RefreshTokenReadOnly> CreateRefreshToken(Guid userId, string token, DateTime expires)
        {
            RefreshToken refreshToken = new RefreshToken
            {
                UserAccountId = userId,
                Token = token,
                ExpiresOnUtc = expires,
            };

            await _userAccountInfoRepository.SaveRefreshToken(refreshToken);

            return new RefreshTokenReadOnly()
            {
                Id = refreshToken.Id,
                UserAccountId = refreshToken.UserAccountId,
                Token = refreshToken.Token,
                ExpiresOnUtc = refreshToken.ExpiresOnUtc
            };
        }

        public async Task<RefreshTokenReadOnly> UpdateRefreshToken(string oldToken, string newToken, DateTime expires)
        {
            RefreshToken? refreshToken = await _userAccountInfoRepository.GetRefreshToken(oldToken);

            if (refreshToken == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "INVALID_REFRESH_TOKEN");
            }

            if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new HttpResponseException((int)HttpStatusCode.Unauthorized, "REFRESH_TOKEN_EXPIRED");
            }

            refreshToken.Token = newToken;

            await _userAccountInfoRepository.SaveRefreshToken(refreshToken);

            return new RefreshTokenReadOnly()
            {
                Id = refreshToken.Id,
                UserAccountId = refreshToken.UserAccountId,
                Token = refreshToken.Token,
                ExpiresOnUtc = refreshToken.ExpiresOnUtc,
            };
        }

        public async Task<ProfileAvatarReadOnlyDto> UpdateUserAvatar(Guid userAccountId, ProfileAvatarDto profileAvatarDto)
        {
            List<string> validExtensions = new List<string>() { ".jpg", ".jpeg", ".png", "gif" };
            IFormFile file = profileAvatarDto.Avatar;

            string extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension))
            {
                throw new HttpResponseException(400);
            }

            long size = file.Length;

            if (size > 5 * 1024 * 1024)
            {
                throw new HttpResponseException(400);
            }

            string dir = Path.Combine(Directory.GetCurrentDirectory());
            string path = Path.Combine("Media", "Avatar");
            string fileName = Guid.NewGuid().ToString() + extension;
            string filePath = Path.Combine(path, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.Combine(dir, path));

            var user = await _userAccountInfoRepository.GetUserById(userAccountId);

            // Delete the old avatar if it exists
            if (!string.IsNullOrEmpty(user.AvatarPath))
            {
                string oldFilePath = Path.Combine(dir, user.AvatarPath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            using FileStream stream = new FileStream(Path.Combine(dir, filePath), FileMode.Create);
            file.CopyTo(stream);

            user.AvatarPath = filePath;
            await _userAccountInfoRepository.SaveUser(user);

            return new ProfileAvatarReadOnlyDto()
            {
                FilePath = filePath
            };
        }
    }
}
