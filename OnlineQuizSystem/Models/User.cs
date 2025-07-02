namespace OnlineQuizSystem.Models;

public class User
{
    public int id { get; set; }
    public string username { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    // Navigation properties can be added here if needed
    // public ICollection<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();
    
}