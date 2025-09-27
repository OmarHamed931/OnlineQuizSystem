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
            },
            CategoryId = SeedIds.Geography // Assign a valid Category if categories are implemented
            
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
            },
            CategoryId = SeedIds.Mathematics // Assign a valid CategoryId if categories are implemented
        };
        var Q3 = new Question
        {
            Text = "The Earth is flat.",
            Type = Question.QuestionType.TrueFalse,
            CorrectAnswer = false,
            CategoryId = SeedIds.GeneralKnowledge// Assign a valid CategoryId if categories are implemented
        };
        var Q4 = new Question
        {
            Text = "Who wrote 'Hamlet'?",
            Type = Question.QuestionType.ShortAnswer,
            Answer = "William Shakespeare",
            CategoryId = SeedIds.Literature // Assign a valid CategoryId if categories are implemented
        };
        await _context.Questions.AddRangeAsync(Q1, Q2, Q3, Q4);
        await _context.SaveChangesAsync();
    }
    public static async Task SeedCategoriesAsync(AppDbContext _context)
    {
        if (await _context.Categories.AnyAsync())
            return;
        var C1 = new Category
        {
            Id = SeedIds.Geography,
            Name = "Geography",
            Description = "Questions related to geographical locations and features."
        };
        var C2 = new Category
        {
            Id = SeedIds.Mathematics,
            Name = "Mathematics",
            Description = "Questions related to numbers, calculations, and mathematical concepts."
        };
        var C3 = new Category
        {
            Id = SeedIds.GeneralKnowledge,
            Name = "General Knowledge",
            Description = "A mix of various general knowledge questions."
        };
        var C4 = new Category
        {
            Id = SeedIds.Literature,
            Name = "Literature",
            Description = "Questions about books, authors, and literary works."
        };
        await _context.Categories.AddRangeAsync(C1, C2, C3, C4);
        await _context.SaveChangesAsync();
    }
    
}