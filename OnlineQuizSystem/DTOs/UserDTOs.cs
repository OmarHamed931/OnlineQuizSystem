using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.DTOs;

public class UserDTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
        [MinLength(6, ErrorMessage = "Email must be at least 6 characters long.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        public string InputPassword { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Default role is User
        
    }

    public class LoginUserDTO
    {
        [MinLength(6, ErrorMessage = "Email must be at least 6 characters long.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        public string InputPassword { get; set; } = string.Empty;
    }
    public class UserDTO
    {
        public string? Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Default role is User
        public string? Token { get; set; } = string.Empty; // JWT token for authentication
    }
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
     
}