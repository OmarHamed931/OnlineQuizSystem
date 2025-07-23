using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.AuthService;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;  
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DTOs.UserDTOs.RegisterUserDTO registerUserDTO)
    {
        
        return BadRequest("Registration not implemented yet.");
        
        // authentication service will be used to register the user (not implemented yet)
    }
    
    
    

    
    
}