using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizSystem.Models;

[Index(nameof(Email), IsUnique = true)]

public class User
{    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; }
    public string Name { get; set; } 
    [Required, MinLength(6, ErrorMessage = "Email must be at least 6 characters long.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string Role { get; set; } // can be "Admin" or "User" for now 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    

}

    