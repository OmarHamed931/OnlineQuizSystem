using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public interface IQuestionRepo
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<Question?> GetQuestionByIdAsync(Guid id);
    Task <Question> AddQuestionAsync(Question question);
    
    Task <Question> UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(Guid id);
    
    Task<IEnumerable<Question>> GetQuestionsByCategoryIdAsync(Guid categoryId);
    
    
    /*
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
*/
    
}