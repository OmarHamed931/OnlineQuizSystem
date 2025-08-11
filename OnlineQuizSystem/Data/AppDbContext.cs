namespace OnlineQuizSystem.Data;
using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // public DbSet<Models.Quiz> Quizzes { get; set; }
    // public DbSet<Models.Question> Questions { get; set; }
    // public DbSet<Models.Answer> Answers { get; set; }
    public DbSet<User> Users { get; set; }
    // public DbSet<Models.UserQuiz> UserQuizzes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure the User entity
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Add any additional configurations for other entities here
    }
}
// Uncomment the DbSet properties above when you have defined the corresponding models in the Models namespace.