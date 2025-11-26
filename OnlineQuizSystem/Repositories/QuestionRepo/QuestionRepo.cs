using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.QuestionRepo;

public class QuestionRepo(AppDbContext _context) : IQuestionRepo
{
    public async Task<IEnumerable<QuestionDTOs.QuestionResponseDTO>> GetAllQuestionsAsync()
    {
        return await _context.Questions
            .AsNoTracking()
            .Select(q => new QuestionDTOs.QuestionResponseDTO(
            q.Id,
            q.Text,
            q.ImageUrl,
            q.Type,
            q.Choices.ToList(),
            q.CorrectAnswer,
            q.Answer,
            q.Points,
            q.Category != null ? q.Category.Id : null,
            q.Category != null ? q.Category.Name : null
        )).ToListAsync();
    }

    public async Task<QuestionDTOs.QuestionResponseDTO?> GetQuestionByIdAsync(Guid id)
    {
        var question = await _context.Questions.Where(q => q.Id == id)
            .AsNoTracking()
            .Select(q => new QuestionDTOs.QuestionResponseDTO(
                q.Id,
                q.Text,
                q.ImageUrl,
                q.Type,
                q.Choices.ToList(),
                q.CorrectAnswer,
                q.Answer,
                q.Points,
                q.Category != null ? q.Category.Id : null,
                q.Category != null ? q.Category.Name : null
            )).FirstOrDefaultAsync();
        return question;
    }

    public async Task<Question> AddQuestionAsync(Question question)
    {
        // might need to use DTOs for better query performance
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        var response = _context.Questions.Include(q => q.Category).FirstOrDefault(q => q.Id == question.Id);
        return response;
    }

    // Uncomment if you want to implement update functionality

    public async Task<Question> UpdateQuestionAsync(Guid id, Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        var response = _context.Questions.Include(q => q.Category).FirstOrDefault(q => q.Id == question.Id);
        return response;
        
     
        
    }



    // Uncomment if you want to implement delete functionality
    public async Task DeleteQuestionAsync(Guid id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Question>> GetQuestionsByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Questions
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync();
    }
    public async Task<Question?> GetQuestionEntityByIdAsync(Guid id)
    {
        return await _context.Questions.FindAsync(id);
    }
    
}