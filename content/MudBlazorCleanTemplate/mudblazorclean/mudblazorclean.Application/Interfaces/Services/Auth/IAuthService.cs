using System.Security.Claims;
using mudblazorclean.Application.Common;
using mudblazorclean.Application.DTOs.Requests.Auth;

namespace mudblazorclean.Application.Interfaces.Services.Authentication
{
    public interface IAuthService
    {
        OperationResult<List<Claim>> Login(LoginRequest request);

        Task<OperationResult<string>> Registration(RegistrationRequest request);
    }
}
