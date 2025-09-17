using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using apiclean.Application.Common;
using apiclean.Application.DTOs.Requests.Auth;
using apiclean.Application.DTOs.Responses.Auth;
using apiclean.Application.Interfaces.Repositories;
using apiclean.Application.Interfaces.Services.Authentication;
using apiclean.Application.Interfaces.UnitOfWork;
using apiclean.Application.Util;
using apiclean.Domain.Entities;

namespace apiclean.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IUnitOfWork unitOfWork
        )
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<TokenResponse>> Login(LoginRequest request)
        {
            var user = _userRepository.GetByUsernameAsync(request.Username).Result;
            if (
                user == null
                || !SecurityHelper.VerifyPassword(request.Password, user.HashedPassword)
            )
            {
                return OperationResult<TokenResponse>.Unauthorized(
                    "Username or password is incorrect"
                );
            }

            var tokenResponse = await GenerateTokensForUser(user);

            return OperationResult<TokenResponse>.Ok(tokenResponse);
        }

        public async Task<OperationResult<string>> Registration(RegistrationRequest request)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.UserName);

            if (existingUser != null)
            {
                return OperationResult<string>.Conflict("Username is already taken");
            }

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                return OperationResult<string>.Conflict("Email is already registered");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return OperationResult<string>.BadRequest("Passwords do not match");
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var newUser = new User
            {
                Username = request.UserName,
                Email = request.Email,
                HashedPassword = hashedPassword,
            };

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<string>.Created("User registered successfully");
        }

        public async Task<OperationResult<TokenResponse>> RefreshingToken(TokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return OperationResult<TokenResponse>.Unauthorized("Invalid access token");
            }

            var username = principal.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return OperationResult<TokenResponse>.Unauthorized("Invalid access token");
            }

            var id = principal.FindFirst("id")?.Value;

            if (id == null || !Guid.TryParse(id, out _))
            {
                return OperationResult<TokenResponse>.BadRequest("Invalid access token.");
            }

            var user = await _userRepository.GetEntityByIdAsync(Guid.Parse(id));
            if (
                user == null
                || user.RefreshToken != request.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow
            )
            {
                return OperationResult<TokenResponse>.Unauthorized("Invalid refresh token");
            }

            var tokenResponse = await GenerateTokensForUser(user);

            return OperationResult<TokenResponse>.Ok(tokenResponse);
        }

        private async Task<TokenResponse> GenerateTokensForUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponse
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken,
            };
        }
    }
}
