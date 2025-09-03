using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.UserRepo;

public interface IUserRepo
{
    // not to use IdentityUser directly, but to use the User model
    // not to return strings 
    public Task<UserDTOs.UserDTO> RegisterUserAsync(User user);
    public Task<User> UpdateUserAsync(User user);
    
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<bool> IsEmailExistsAsync(string email);
    
    public Task<bool> IsUserExistsAsync(string userId);
    
    
}