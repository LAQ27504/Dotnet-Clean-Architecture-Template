using System.ComponentModel.DataAnnotations;

namespace blazorclean.Application.DTOs.Requests.Auth
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must have at least one uppercase, one lowercase, one number, and one special character."
        )]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
