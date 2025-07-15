using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class authController : Controller
{
    private readonly UserManager<Models.User> _userManager;
    private readonly SignInManager<Models.User> _signInManager;
    
    public authController(UserManager<Models.User> userManager, SignInManager<Models.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DTOs.UserDTOs.RegisterUserDTO registerUserDTO)
    {
        if (registerUserDTO == null || string.IsNullOrEmpty(registerUserDTO.Email) || string.IsNullOrEmpty(registerUserDTO.Password))
        {
            return BadRequest("Invalid registration data.");
        }
        return BadRequest("Registration not implemented yet.");
        
        // authentication service will be used to register the user (not implemented yet)
    }
    
    
    

    
    
}