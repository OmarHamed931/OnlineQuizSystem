using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Repositories.UserRepo;
using OnlineQuizSystem.Services.JWTService;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Services.EmailService;
using OnlineQuizSystem.Services.OtpService;


namespace OnlineQuizSystem.Services.AuthService;

public class AuthService : IAuthService
{
    // remember to not use IdentityUser and redefine your own User model
    private readonly ITokenService _tokenService;
    private readonly IUserRepo _userRepo;
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    
    public AuthService(ITokenService tokenService , IUserRepo userRepo, IEmailService emailService, IOtpService otpService)
    {
        _tokenService = tokenService;
        _userRepo = userRepo;
        _emailService = emailService;
        _otpService = otpService;
    }
    public Task<UserDTOs.UserDTO> RegisterUserAsync(UserDTOs.RegisterUserDTO UserDto)
    {
        var User = new User
        {
            Email = UserDto.Email,
            Name = UserDto.Name,
            Role = UserDto.Role,
            
        };
        // Generate a salt for the password
        User.Salt = GenerateSalt();
        // Hash the password with the salt
        User.PasswordHash = HashPassword(UserDto.InputPassword, User.Salt);
        bool isEmailExists = _userRepo.IsEmailExistsAsync(UserDto.Email).Result;
        if (isEmailExists)
        {
            throw new Exception("Email already exists");
        }

        return _userRepo.RegisterUserAsync(User);
    }
    public Task<UserDTOs.UserDTO> LoginUserAsync(UserDTOs.LoginUserDTO UserDto)
    {
        var User = _userRepo.GetUserByEmailAsync(UserDto.Email).Result;
        if (User == null)
        {
            throw new Exception("either email or password is incorrect");
        }
        // Hash the input password with the user's salt
        var HashedInputPassword = HashPassword(UserDto.InputPassword, User.Salt);
        if (HashedInputPassword == User.PasswordHash)
        {
            // Password matches, generate JWT token
            var token = _tokenService.GenerateToken(User);
            return Task.FromResult(new UserDTOs.UserDTO
            {
                Id = User.Id.ToString(),
                Email = User.Email,
                Role = User.Role,
                Token = token
            });
        }
        else
        {
            throw new Exception("either email or password is incorrect");
        }
    }

    public async Task<bool> ChangePasswordAsync(string userId, UserDTOs.ChangePasswordDTO changePasswordDto)
    {
        var user = _userRepo.GetUserByIdAsync(Guid.Parse(userId)).Result;
        if (user == null)
        {
            throw new Exception("User not found");
        }
        // Hash the current password with the user's salt
        var hashedCurrentPassword = HashPassword(changePasswordDto.CurrentPassword, user.Salt);
        if (hashedCurrentPassword != user.PasswordHash)
            return false; // Current password does not match
        // Generate a new salt and hash the new password
        user.Salt = GenerateSalt();
        user.PasswordHash = HashPassword(changePasswordDto.NewPassword, user.Salt);
        user.UpdatedAt = DateTime.UtcNow;
        // Update the user in the database
        // Assuming _userRepo has an UpdateUserAsync method
        var updatedUser = await _userRepo.UpdateUserAsync(user);
        if (updatedUser != null)
            return true;
        throw new Exception("Failed to update password");
    }
    
    public async Task<bool> RequestResetPasswordAsync(string userEmail)
    {
        var user = await _userRepo.GetUserByEmailAsync(userEmail);
        if (user == null)
            throw new Exception("User not found");
        // This method would typically send a password reset email to the user
        var otp = _otpService.GenerateOtp(userEmail);
        // Send email to userEmail with OTP password reset code
        await _emailService.SendEmailAsync(userEmail, otp);
        return true;
        
    }

    public async Task ResetPasswordAsync(UserDTOs.ResetPasswordDTO resetPasswordDto)
    {
        var user = await _userRepo.GetUserByEmailAsync(resetPasswordDto.Email);
        if (user == null)
            throw new Exception("User not found");
        // Verify the OTP
        var isOtpValid = _otpService.ValidateOtp(resetPasswordDto.Otp,resetPasswordDto.Email);
        if (!isOtpValid)
            throw new Exception("Invalid OTP");
        // Generate a new salt and hash the new password
        user.Salt = GenerateSalt();
        user.PasswordHash = HashPassword(resetPasswordDto.NewPassword, user.Salt);
        user.UpdatedAt = DateTime.UtcNow;
        // Update the user in the database
        var updatedUser = await _userRepo.UpdateUserAsync(user);
        if (updatedUser != null)
            return;
        throw new Exception("Failed to update password");
    }

    private string GenerateSalt()
    {
        // Generate a random salt for password hashing
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        char[] saltChars = new char[32];
        for (int i = 0; i < saltChars.Length; i++)
        {
            saltChars[i] = Chars[random.Next(Chars.Length)];
        }
        return new string(saltChars);
    }
    private string HashPassword(string password, string salt)
    {
        var saltedPassword = password + salt;
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        

    }
}