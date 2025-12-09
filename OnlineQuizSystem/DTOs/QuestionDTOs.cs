using System.ComponentModel.DataAnnotations;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.DTOs;

public class QuestionDTOs
{
    public class QuestionDTO
    {
        [Required]
        public string Text { get; set; } 
        public string? ImageURL { get; set; } // Optional image URL for the question
        [Required]
        public Question.QuestionType Type { get; set; }
        public List<CreateChoiceDTO> Choices { get; set; }
        public bool? CorrectAnswer { get; set; } // For True/False questions
        public string? Answer { get; set; } // For Short Answer questions
        public int Points { get; set; } // Default points for the question
        public Guid? CategoryId { get; set; } // Foreign key to Category

        public QuestionDTO(string text, Question.QuestionType type, List<CreateChoiceDTO?> choices, bool? correctAnswer, string? answer,Guid? categoryId,int points = 1, string? imageURL = null)
        {
            Text = text;
            ImageURL = imageURL;
            Type = type;
            Choices = choices.Where(c => c != null).Select(c => new CreateChoiceDTO(c!.Text, c.IsCorrect)).ToList();
            CorrectAnswer = correctAnswer;
            Answer = answer;
            Points = points;
            CategoryId = categoryId;
        }
    }
    public class QuestionResponseDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } 
        public string? ImageURL { get; set; } // Optional image URL for the question
        public Question.QuestionType Type { get; set; }
        public List<Choice?> Choices { get; set; } // Collection of choices for Single and Multiple Choice questions with correct answers
        public bool? CorrectAnswer { get; set; } // For True/False questions
        public string? Answer { get; set; } // For Short Answer questions
        public int Points { get; set; }  // Default points for the question
        public Guid? CategoryId { get; set; } // Foreign key to Category
        public string? CategoryName { get; set; } // Convenience property to access category name

        public QuestionResponseDTO(Guid id, string text, string? imageURL, Question.QuestionType type, List<Choice?> choices, bool? correctAnswer, string? answer, int points,Guid? categoryId, string? categoryName)
        {
            Id = id;
            Text = text;
            ImageURL = imageURL;
            Type = type;
            Choices = choices;
            CorrectAnswer = correctAnswer;
            Answer = answer;
            Points = points;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
        
    }
    
    
    }
    
    public record CreateChoiceDTO(string Text, bool IsCorrect);
    // public record UpdateChoiceDTO(int Id, string Text, bool IsCorrect);
    // public record ChoiceResponseDTO(int Id, string Text, bool IsCorrect);
    
