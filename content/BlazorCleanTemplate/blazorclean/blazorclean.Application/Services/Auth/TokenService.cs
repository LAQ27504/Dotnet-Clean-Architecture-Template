using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using blazorclean.Application.Interfaces.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace blazorclean.Application.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var issuer = _configuration["JwtSettings:Issuer"] ?? "blazorclean";
            var audience = _configuration["JwtSettings:Audience"] ?? "blazorcleanUser";
            var secretKey = _configuration["JwtSettings:SecretKey"] ?? "YourSuperSecretKey12345";
            int expireTime;
            if (int.TryParse(_configuration["JwtSettings:ExpireTime"], out int result))
            {
                expireTime = result;
            }
            else
            {
                expireTime = 5;
            }
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireTime),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"] ?? "YourSuperSecretKey12345";

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role,
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out securityToken
            );
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
