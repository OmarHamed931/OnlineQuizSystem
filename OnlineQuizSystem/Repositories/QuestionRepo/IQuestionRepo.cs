using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public interface IQuestionRepo
{
    Task<IEnumerable<QuestionDTOs.QuestionResponseDTO>> GetAllQuestionsAsync();
    Task<QuestionDTOs.QuestionResponseDTO?> GetQuestionByIdAsync(Guid id);
    Task <Question> AddQuestionAsync(Question question);
    
    Task <Question> UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(Guid id);
    
    Task<IEnumerable<Question>> GetQuestionsByCategoryIdAsync(Guid categoryId);
    
    
    /*
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
*/
    
}