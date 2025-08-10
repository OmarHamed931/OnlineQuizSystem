using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.JWTService;

public interface ITokenService
{
    public string GenerateToken(User User); 
}