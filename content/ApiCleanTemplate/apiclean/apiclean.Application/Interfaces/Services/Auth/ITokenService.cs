using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiclean.Application.Interfaces.Services.Authentication
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
