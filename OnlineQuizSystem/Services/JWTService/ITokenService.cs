namespace OnlineQuizSystem.Services.JWTService;

public interface ITokenService
{
    public string GenerateToken(string userId, string email, bool isAdmin); 
}