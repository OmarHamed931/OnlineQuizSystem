using Microsoft.Build.Framework;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.DTOs;

public class QuestionDTOs
{
    public record CreateQuestionDTO(string Text,string? ImageURL, Question.QuestionType Type, List<CreateChoiceDTO?> Choices, bool? CorrectAnswer = null,string? Answer = null, int Points = 1);
    // public record UpdateQuestionDTO(string Text, List<UpdateChoiceDTO> Choices);
    // public record QuestionResponseDTO(int Id, string Text, List<ChoiceResponseDTO> Choices);
    
    public record CreateChoiceDTO(string Text, bool IsCorrect);
    // public record UpdateChoiceDTO(int Id, string Text, bool IsCorrect);
    // public record ChoiceResponseDTO(int Id, string Text, bool IsCorrect);
    
}