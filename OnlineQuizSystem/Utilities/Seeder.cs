using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Utilities;

public static class Seeder
{
    public static async Task SeedQuestionsAsync(AppDbContext _context)
    {
        if (await _context.Questions.AnyAsync())
            return;
        var Q1 = new Question
        {
            Text = "What is the capital of France?",
            Type = Question.QuestionType.SingleChoice,
            Choices = new List<Choice?>
            {
                new Choice { Text = "Berlin", IsCorrect = false },
                new Choice { Text = "Madrid", IsCorrect = false },
                new Choice { Text = "Paris", IsCorrect = true },
                new Choice { Text = "Rome", IsCorrect = false }
            }
        };
        var Q2 = new Question
        {
            Text = "Select all prime numbers.",
            Type = Question.QuestionType.MultipleChoice,
            Choices = new List<Choice?>
            {
                new Choice { Text = "2", IsCorrect = true },
                new Choice { Text = "3", IsCorrect = true },
                new Choice { Text = "4", IsCorrect = false },
                new Choice { Text = "5", IsCorrect = true }
            }
        };
        var Q3 = new Question
        {
            Text = "The Earth is flat.",
            Type = Question.QuestionType.TrueFalse,
            CorrectAnswer = false
        };
        var Q4 = new Question
        {
            Text = "Who wrote 'Hamlet'?",
            Type = Question.QuestionType.ShortAnswer,
            Answer = "William Shakespeare"
        };
        await _context.Questions.AddRangeAsync(Q1, Q2, Q3, Q4);
        await _context.SaveChangesAsync();
    }
    
}