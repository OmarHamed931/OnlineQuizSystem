using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.UserRepo;

public interface IUserRepo
{
    // not to use IdentityUser directly, but to use the User model
    // not to return strings 
    public Task<string> RegisterUserAsync(User user , string inputPassword);
    public Task<string> LoginUserAsync(User user);
}