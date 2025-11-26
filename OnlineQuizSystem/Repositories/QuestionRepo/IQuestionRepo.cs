using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public interface IQuestionRepo
{
    Task<IEnumerable<QuestionDTOs.QuestionResponseDTO>> GetAllQuestionsAsync();
    Task<QuestionDTOs.QuestionResponseDTO?> GetQuestionByIdAsync(Guid id);
    Task<Question> GetQuestionEntityByIdAsync(Guid id);
    Task <Question> AddQuestionAsync(Question question);
    
    Task <Question> UpdateQuestionAsync(Guid id ,Question question);
    Task DeleteQuestionAsync(Guid id);
    
    Task<IEnumerable<Question>> GetQuestionsByCategoryIdAsync(Guid categoryId);
    
    
    /*
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
*/
    
}