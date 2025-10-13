using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace OnlineQuizSystem.Models;

public class Question
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } // Optional image URL for the question
    public int Points { get; set; } = 1; // Default points for the question
    public Guid? CategoryId { get; set; } // Foreign key to Category
    public string? CategoryName => Category?.Name; // Convenience property to access category name
    [JsonIgnore]
    public Category? Category { get; set; } // Navigation property to Category (potential reference cycle, can be deleted because it's not used)
    
    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice,
        ShortAnswer,
        TrueFalse
    }
    public QuestionType Type { get; set; }
    public ICollection<Choice?> Choices { get; set; } = new List<Choice>(); // Collection of choices for Single and Multiple Choice questions with correct answers
    
    public string? Answer { get; set; } = string.Empty; // For Short Answer questions
    public bool? CorrectAnswer { get; set; } // For True/False questions
}
