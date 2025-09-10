namespace apiclean.Application.DTOs.Requests.Auth
{
    public class RegistrationRequest
    {
        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }
    }
}
