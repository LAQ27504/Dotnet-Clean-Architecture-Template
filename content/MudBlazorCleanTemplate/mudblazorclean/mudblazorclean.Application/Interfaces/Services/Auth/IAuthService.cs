using mudblazorclean.Application.Common;
using mudblazorclean.Application.DTOs.Requests.Auth;
using mudblazorclean.Application.DTOs.Responses.Auth;

namespace mudblazorclean.Application.Interfaces.Services.Authentication
{
    public interface IAuthService
    {
        Task<OperationResult<TokenResponse>> Login(LoginRequest request);

        Task<OperationResult<string>> Registration(RegistrationRequest request);

        Task<OperationResult<TokenResponse>> RefreshingToken(TokenRequest request);
    }
}
