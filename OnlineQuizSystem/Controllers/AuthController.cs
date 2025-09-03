using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.AuthService;
using System.Security.Claims;

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
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] DTOs.UserDTOs.LoginUserDTO LoginUserDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid user data.");
        }
        try
        {
            var user = await _AuthService.LoginUserAsync(LoginUserDTO);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch("change-password")]
    [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] DTOs.UserDTOs.ChangePasswordDTO ChangePasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found.");
            }
            await _AuthService.ChangePasswordAsync(userId, ChangePasswordDTO);
            return Ok("Password changed successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("reset-password")]
    [Authorize]
    public async Task<IActionResult> ResetPassword()
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
            throw new Exception("User email not found");
        // send email to userEmail with OTP password reset code
        await _AuthService.GetResetPasswordAsync(userEmail);
        return Ok("If the email exists, a password reset code has been sent.");
        
        
    }
    
    
    
    

    
    
}