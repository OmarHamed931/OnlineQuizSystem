using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.DTOs;

public class UserDTOs
{
    public class RegisterUserDTO
    {
        [MinLength(6, ErrorMessage = "Email must be at least 6 characters long.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }

    public class LoginUserDTO
    {
        
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false; // can be string Role 
    }
     
}