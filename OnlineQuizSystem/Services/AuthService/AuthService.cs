using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Repositories.UserRepo;
using OnlineQuizSystem.Services.JWTService;
using OnlineQuizSystem.Models;



namespace OnlineQuizSystem.Services.AuthService;

public class AuthService : IAuthService
{
    // remember to not use IdentityUser and redefine your own User model
    private readonly ITokenService _tokenService;
    private readonly IUserRepo _userRepo;
    
    public AuthService(ITokenService tokenService , IUserRepo userRepo)
    {
        _tokenService = tokenService;
        _userRepo = userRepo;
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