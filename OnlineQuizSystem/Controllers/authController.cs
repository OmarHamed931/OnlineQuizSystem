using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.AuthService;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class authController : Controller
{
    private readonly IAuthService _authService;
    
    public authController(IAuthService authService)
    {
        _authService = authService;
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