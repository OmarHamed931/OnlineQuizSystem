using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public class QuestionRepo(AppDbContext _context) : IQuestionRepo
{
    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        return await _context.Questions.ToListAsync();
    }

    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        return await _context.Questions.FindAsync(id);
    }

    public async Task<Question> AddQuestionAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    // Uncomment if you want to implement update functionality
    /*
    public async Task UpdateQuestionAsync(Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }
    */

    // Uncomment if you want to implement delete functionality
    /*
    public async Task DeleteQuestionAsync(int id)
    {
        var question = await GetQuestionByIdAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
    */
}