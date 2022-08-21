using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LostMap.BLL.Auth
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }

        public SymmetricSecurityKey AccessTokenSymmetricSecurityKey =>
            new(Encoding.ASCII.GetBytes(Key));

        public TokenValidationParameters ValidationParameters =>
            new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = AccessTokenSymmetricSecurityKey,
                ClockSkew = TimeSpan.Zero
            };
    }
}
