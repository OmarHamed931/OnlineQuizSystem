using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.QuestionService;

public interface IQuestionService
{
    // will be refactored once quiz service is ready

    public Task<Question> AddQuestionAsync(QuestionDTOs.CreateQuestionDTO questionDto);
    public Task<Question> GetQuestionByIdAsync(string id);
    public Task<IEnumerable<Question>> GetAllQuestionsAsync();
    
    public Task<bool> VerifyAnswerAsync(string questionId, List<string> answer);
}