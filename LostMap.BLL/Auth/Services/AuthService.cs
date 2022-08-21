using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Auth.Models;
using LostMap.Common.Utils;
using LostMap.DAL.InitData;
using LostMap.DAL.Interfaces;
using LostMap.DAL.Models;
using System.Security.Cryptography;

namespace LostMap.BLL.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly AuthConfig _authConfig;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService,
            AuthConfig authConfig)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _authConfig = authConfig;
        }

        public async Task<UserAuthData> Login(LoginRequest loginData)
        {
            var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Email == loginData.Email);
            if (user == null)
            {
                throw new Exception();
            }

            var isPasswordValid = PasswordHasher.VerifyPassword(loginData.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception();
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_authConfig.RefreshTokenExpirationDays);
            await _unitOfWork.SaveAsync();

            return GenerateUserAuthData(user);
        }

        public async Task<UserAuthData> SignUp(SignUpRequest signUpRequest)
        {
            var existingUser = await _unitOfWork.UserRepository
                .GetSingleAsync(u => u.Email == signUpRequest.Email, useTracking: false);
            if (existingUser != null)
            {
                throw new Exception("Email is registered");
            }

            var user = new User
            {
                Email = signUpRequest.Email,
                Name = signUpRequest.Name,
                LastName = signUpRequest.LastName,
                PasswordHash = PasswordHasher.HashPassword(signUpRequest.Password),
                RoleId = RoleData.Ids.User,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_authConfig.RefreshTokenExpirationDays)
            };
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveAsync();

            return GenerateUserAuthData(user);
        }

        public async Task<UserAuthData> RecreateTokens(User user, string refreshToken)
        {
            if (user == null)
            {
                throw new Exception();
            }

            if (user.RefreshToken != refreshToken ||
                DateTime.UtcNow >= user.RefreshTokenExpiryTime)
            {
                throw new Exception("Refresh token is invalid or expired");
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_authConfig.RefreshTokenExpirationDays);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return GenerateUserAuthData(user);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private UserAuthData GenerateUserAuthData(User user) => new()
        {
            Email = user.Email,
            AccessToken = _jwtTokenService.GenerateAccessToken(user),
            RefreshToken = user.RefreshToken
        };

        public async Task Logout(User user)
        {
            if (user != null)
            {
                RevokeRefreshToken(user);
                await _unitOfWork.SaveAsync();
            }
        }

        private void RevokeRefreshToken(User user)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            _unitOfWork.UserRepository.Update(user);
        }

        public async Task<User?> GetByAccessToken(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                return null;

            var userId = _jwtTokenService.GetClaimValue(accessToken, CustomClaimTypes.Id);

            return string.IsNullOrWhiteSpace(userId)
                ? null
                : await _unitOfWork.UserRepository.GetSingleAsync(
                    u => u.Id == Guid.Parse(userId), useTracking: false);
        }
    }
}
