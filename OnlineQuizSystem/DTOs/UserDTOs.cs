namespace OnlineQuizSystem.DTOs;

public class UserDTOs
{
    public class RegisterUserDTO
    {

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserDTO
    {
        
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
     
}