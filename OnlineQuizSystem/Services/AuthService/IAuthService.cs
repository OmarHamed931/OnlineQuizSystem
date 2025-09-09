using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.AuthService;

public interface IAuthService
{
    public Task<UserDTOs.UserDTO> RegisterUserAsync(UserDTOs.RegisterUserDTO userDto);
    public Task<UserDTOs.UserDTO> LoginUserAsync(UserDTOs.LoginUserDTO userDto);
    public Task<bool> ChangePasswordAsync(string userId,UserDTOs.ChangePasswordDTO changePasswordDto);
    public Task<bool> RequestResetPasswordAsync(string email); // might consider using only Task
    public Task ResetPasswordAsync(UserDTOs.ResetPasswordDTO resetPasswordDto);
}