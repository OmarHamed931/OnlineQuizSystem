using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.AuthService;

public interface IAuthService
{
    public Task<UserDTOs.UserDTO> RegisterUserAsync(UserDTOs.RegisterUserDTO userDto);
    public Task<UserDTOs.UserDTO> LoginUserAsync(UserDTOs.LoginUserDTO userDto);
}