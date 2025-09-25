using System.Security.Claims;
using mudblazorclean.Application.Common;
using mudblazorclean.Application.DTOs.Requests.Auth;
using mudblazorclean.Application.Interfaces.Repositories;
using mudblazorclean.Application.Interfaces.Services.Authentication;
using mudblazorclean.Application.Interfaces.UnitOfWork;
using mudblazorclean.Application.Util;
using mudblazorclean.Domain.Entities;

namespace mudblazorclean.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork
        )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public OperationResult<List<Claim>> Login(LoginRequest request)
        {
            var user = _userRepository.GetByUsernameAsync(request.Username).Result;
            if (
                user == null
                || !SecurityHelper.VerifyPassword(request.Password, user.HashedPassword)
            )
            {
                return OperationResult<List<Claim>>.Unauthorized(
                    "Username or password is incorrect"
                );
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            return OperationResult<List<Claim>>.Ok(claims);
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
    }
}
