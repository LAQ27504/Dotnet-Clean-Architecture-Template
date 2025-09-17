using blazorclean.Application.Common;
using blazorclean.Application.DTOs.Requests.Auth;
using blazorclean.Application.DTOs.Responses.Auth;

namespace blazorclean.Application.Interfaces.Services.Authentication
{
    public interface IAuthService
    {
        Task<OperationResult<TokenResponse>> Login(LoginRequest request);

        Task<OperationResult<string>> Registration(RegistrationRequest request);

        Task<OperationResult<TokenResponse>> RefreshingToken(TokenRequest request);
    }
}
