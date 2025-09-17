using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace blazorclean.Application.Interfaces.Services.Authentication
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
