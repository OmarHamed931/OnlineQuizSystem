using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OnlineQuizSystem.Services.JWTService;

public class TokenService : ITokenService
{
    private readonly IConfigurationBuilder _configurationBuilder;
    public TokenService(IConfigurationBuilder configurationBuilder)
    {
        _configurationBuilder = configurationBuilder;
    }
    
    public string GenerateToken(string userId, string email, bool isAdmin)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationBuilder.Build().GetSection("Jwt:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configurationBuilder.Build().GetSection("Jwt:Issuer").Value,
            audience: _configurationBuilder.Build().GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}