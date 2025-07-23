using Microsoft.AspNetCore.Identity;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Repositories.UserRepo;
using OnlineQuizSystem.Services.JWTService;


namespace OnlineQuizSystem.Services.AuthService;

public class AuthService : IAuthService
{
    // remember to not use IdentityUser and redefine your own User model
    private readonly ITokenService _tokenService;
    private readonly IUserRepo _userRepo;
    
    public AuthService(ITokenService tokenService , IUserRepo userRepo)
    {
        _tokenService = tokenService;
        _userRepo = userRepo;
    }
    public Task<string> RegisterUserAsync(UserDTOs.RegisterUserDTO userDto)
    {
        // map userDto to User model
        var user = new Models.User
        {
            Email = userDto.Email,
            UserName = userDto.Email, // Assuming UserName is the same as Email
            
            
        };
        // Call the repository to register the user
        return _userRepo.RegisterUserAsync(user , inputPassword: userDto.Password);
        
    }
    public Task<string> LoginUserAsync(UserDTOs.LoginUserDTO userDto)
    {
        // Implement login logic here
        throw new NotImplementedException("Registration logic not implemented yet.");
    }
}