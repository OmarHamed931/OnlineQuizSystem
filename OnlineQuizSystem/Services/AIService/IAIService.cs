using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.AIService;

public interface IAIService
{
    public Task<AnswerDTOs.ShortAnswerDTO> VerifyAnswer(Question question, string submittedAnswer);
    
}