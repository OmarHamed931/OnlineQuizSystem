using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.AuthService;

public interface IAuthService
{
    public Task<string> RegisterUserAsync(UserDTOs.RegisterUserDTO userDto);
    public Task<string> LoginUserAsync(UserDTOs.LoginUserDTO userDto);
}