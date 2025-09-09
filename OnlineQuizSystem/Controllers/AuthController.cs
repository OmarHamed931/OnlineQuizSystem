using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.AuthService;
using System.Security.Claims;
using OnlineQuizSystem.DTOs;

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
    [HttpPost("reset-password")]
    public async Task<IActionResult> RequestResetPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required.");
        }
        try
        {
            await _AuthService.RequestResetPasswordAsync(email);
            return Ok("If the email exists, a reset link has been sent.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPatch("reset-password")]
    public async Task<IActionResult> ResetPassword(UserDTOs.ResetPasswordDTO resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            await _AuthService.ResetPasswordAsync(resetPasswordDto);
            return Ok("Password has been reset successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    
    
    
    

    
    
}