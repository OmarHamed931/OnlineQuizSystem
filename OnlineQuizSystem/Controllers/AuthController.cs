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
        var result = await _authService.RegisterUserAsync(registerUserDTO);
        if (result == "User registered successfully")
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    
    

    
    
}