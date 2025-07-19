using Microsoft.AspNetCore.Identity;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Services.JWTService;


namespace OnlineQuizSystem.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    public Task<string> RegisterUserAsync(UserDTOs.RegisterUserDTO userDto)
    {
        // map userDto to User model
        var user = new Models.User
        {
            Email = userDto.Email,
            UserName = userDto.Email, // Assuming UserName is the same as Email
        };
        throw new NotImplementedException("Registration logic not implemented yet.");
    }
    public Task<string> LoginUserAsync(UserDTOs.LoginUserDTO userDto)
    {
        // Implement login logic here
        throw new NotImplementedException("Registration logic not implemented yet.");
    }
}