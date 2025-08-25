using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.AIService;

public interface IAIService
{
    public Task<AnswerDTOs.ShortAnswerDTO> VerifyAnswer(string question, string submittedAnswer);
    
}