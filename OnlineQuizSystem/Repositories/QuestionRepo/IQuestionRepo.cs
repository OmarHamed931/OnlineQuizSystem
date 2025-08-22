using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public interface IQuestionRepo
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<Question?> GetQuestionByIdAsync(int id);
    Task <Question> AddQuestionAsync(Question question);
    
    /*Task UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(int id);
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
    Task<IEnumerable<Question>> GetQuestionsByCategoryIdAsync(int categoryId);*/
    
}