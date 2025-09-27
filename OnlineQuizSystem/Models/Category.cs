using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizSystem.Models;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ICollection<Question> Questions { get; set; } = new List<Question>(); // one-to-many relationship with Question

    public int NumberOfQuestions => Questions.Count; // computed property for number of questions in the category
    // future use: public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

}