using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.QuestionService;

public interface IQuestionService
{
    public Task<Question> AddQuestionAsync(QuestionDTOs.CreateQuestionDTO questionDto);
    public Task<Question> GetQuestionByIdAsync(int id);
    public Task<IEnumerable<Question>> GetAllQuestionsAsync();
    
    public Task<bool> VerifyAnswerAsync(int questionId, List<string> answer);
}