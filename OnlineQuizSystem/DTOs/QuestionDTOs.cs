using Microsoft.Build.Framework;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.DTOs;

public class QuestionDTOs
{
    public class CreateQuestionDTO
    {
        [Required]
        public string Text { get; set; } = string.Empty;
        public string? ImageURL { get; set; } // Optional image URL for the question
        [Required]
        public Question.QuestionType Type { get; set; }
        public List<CreateChoiceDTO> Choices { get; set; } = new List<CreateChoiceDTO>();
        public bool? CorrectAnswer { get; set; } // For True/False questions
        public string? Answer { get; set; } = string.Empty; // For Short Answer questions
        public int Points { get; set; } = 1; // Default points for the question
        public Guid? CategoryId { get; set; } // Foreign key to Category

        public CreateQuestionDTO(string text, Question.QuestionType type, List<CreateChoiceDTO> choices, bool? correctAnswer, string? answer, int points, Guid? categoryId, string? imageURL = null)
        {
            Text = text;
            ImageURL = imageURL;
            switch (type)
            {
                case Question.QuestionType.SingleChoice:
                case Question.QuestionType.MultipleChoice:
                    if (choices == null || choices.Count == 0)
                    {
                        throw new ArgumentException("Choices cannot be null or empty for Single and Multiple Choice questions.");
                    }
                    // nullify CorrectAnswer and Answer for these types
                    CorrectAnswer = null;
                    Answer = null;
                    break;
                case Question.QuestionType.TrueFalse:
                    if (correctAnswer == null)
                    {
                        throw new ArgumentException("CorrectAnswer must be provided for True/False questions.");
                    }
                    // nullify Choices and Answer for this type
                    Choices = [];
                    Answer = null;
                    break;
                case Question.QuestionType.ShortAnswer:
                    if (string.IsNullOrWhiteSpace(answer))
                    {
                        throw new ArgumentException("Answer cannot be null or empty for Short Answer questions.");
                    }
                    // nullify Choices and CorrectAnswer for this type
                    Choices = [];
                    CorrectAnswer = null;
                    break;
                default:
                    throw new ArgumentException("Invalid question type.");
            }
            CategoryId = categoryId;
        }
    }
    // public record UpdateQuestionDTO(string Text, List<UpdateChoiceDTO> Choices);
    public class QuestionResponseDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ImageURL { get; set; } // Optional image URL for the question
        public Question.QuestionType Type { get; set; }
        public List<Choice?> Choices { get; set; } = new List<Choice?>();
        public bool? CorrectAnswer { get; set; } // For True/False questions
        public string? Answer { get; set; } = string.Empty; // For Short Answer questions
        public int Points { get; set; } = 1; // Default points for the question
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
    
    public record CreateChoiceDTO(string Text, bool IsCorrect);
    // public record UpdateChoiceDTO(int Id, string Text, bool IsCorrect);
    // public record ChoiceResponseDTO(int Id, string Text, bool IsCorrect);
    
}