using Microsoft.AspNetCore.Identity;

namespace OnlineQuizSystem.Models;

public class User : IdentityUser
{
 bool isAdmin { get; set; } = false;
}