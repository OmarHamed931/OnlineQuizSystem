namespace OnlineQuizSystem.Data;
using Microsoft.EntityFrameworkCore;

public class dbContext : DbContext
{
    public dbContext(DbContextOptions<dbContext> options) : base(options)
    {
    }

    // public DbSet<Models.Quiz> Quizzes { get; set; }
    // public DbSet<Models.Question> Questions { get; set; }
    // public DbSet<Models.Answer> Answers { get; set; }
    public DbSet<Models.User> Users { get; set; }
    // public DbSet<Models.UserQuiz> UserQuizzes { get; set; }
}
// Uncomment the DbSet properties above when you have defined the corresponding models in the Models namespace.