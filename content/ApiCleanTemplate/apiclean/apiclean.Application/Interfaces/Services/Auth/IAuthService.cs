using apiclean.Application.Common;
using apiclean.Application.DTOs.Requests.Auth;
using apiclean.Application.DTOs.Responses.Auth;

namespace apiclean.Application.Interfaces.Services.Authentication
{
    public interface IAuthService
    {
        Task<OperationResult<TokenResponse>> Login(LoginRequest request);

        Task<OperationResult<string>> Registration(RegistrationRequest request);

        Task<OperationResult<TokenResponse>> RefreshingToken(TokenRequest request);
    }
}
