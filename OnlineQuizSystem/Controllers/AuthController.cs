using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.AuthService;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _AuthService;  
    
    public AuthController(IAuthService AuthService)
    {
        _AuthService = AuthService;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DTOs.UserDTOs.RegisterUserDTO RegisterUserDTO)
    {
        // 
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid user data.");
        }
        try
        {
            var user = await _AuthService.RegisterUserAsync(RegisterUserDTO);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
      
    }
    
    
    

    
    
}