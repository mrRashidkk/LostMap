using LostMap.BLL.Auth.Interfaces;
using LostMap.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LostMap.BLL.Auth.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtConfig _config;

        public JwtTokenService(JwtConfig config)
        {
            _config = config;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.ExpirationMinutes),
                Issuer = _config.Issuer,
                Audience = _config.Audience,
                SigningCredentials = new SigningCredentials(
                    _config.AccessTokenSymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }        

        public string? GetClaimValue(string accessToken, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(accessToken);
            return decodedToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
