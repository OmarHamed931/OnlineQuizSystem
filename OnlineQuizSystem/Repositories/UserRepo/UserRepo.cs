using Microsoft.AspNetCore.Identity;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.UserRepo;

public class UserRepo : IUserRepo
{
    private readonly UserManager<Models.User> _userManager;
    private readonly SignInManager<Models.User> _signInManager;
    public UserRepo(UserManager<Models.User> userManager, SignInManager<Models.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // register should return the user details 
    public Task<string> RegisterUserAsync(User user, string inputPassword)
    {
        var result = _userManager.CreateAsync(user, inputPassword).Result;
        return Task.FromResult(result.Succeeded ? "User registered successfully" : string.Join(", ", result.Errors.Select(e => e.Description)));
        //register method might produce a token later 
    }
    public async Task<string> LoginUserAsync(User user)
    {
        var result = await _signInManager.PasswordSignInAsync(user, user.PasswordHash, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return "User logged in successfully";
        }
        else
        {
            return "Login failed";
        }
    }
}