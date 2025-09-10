using apiclean.Application.Common;
using apiclean.Application.DTOs.Requests.Auth;
using apiclean.Application.DTOs.Responses.Auth;
using apiclean.Application.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace apiclean.Presentation.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);
            return ToActionResult(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(MessageResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var result = await _authService.Registration(request);
            return ToActionResult(result);
        }
    }
}
