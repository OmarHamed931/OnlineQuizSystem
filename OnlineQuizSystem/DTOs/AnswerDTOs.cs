namespace OnlineQuizSystem.DTOs;

public class AnswerDTOs
{
    public record ShortAnswerDTO(bool IsCorrect, double Confidence, string? explanation = null);
    public record AnswerTest(string Answer , double Confidence);
}