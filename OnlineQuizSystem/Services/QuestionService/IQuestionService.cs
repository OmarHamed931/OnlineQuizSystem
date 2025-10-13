using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.QuestionService;

public interface IQuestionService
{
    // will be refactored once quiz service is ready

    public Task<Question> AddQuestionAsync(QuestionDTOs.CreateQuestionDTO questionDto);
    public Task<QuestionDTOs.QuestionResponseDTO?> GetQuestionByIdAsync(string id);
    public Task<IEnumerable<QuestionDTOs.QuestionResponseDTO>> GetAllQuestionsAsync();
    
    //public Task<bool> VerifyAnswerAsync(string questionId, List<string> answer);
}