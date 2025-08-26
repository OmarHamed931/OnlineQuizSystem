using Microsoft.AspNetCore.Authorization;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Repositories.QuestionRepo;

namespace OnlineQuizSystem.Services.QuestionService;

public class QuestionService (IQuestionRepo _questionRepo): IQuestionService
{
    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        return await _questionRepo.GetAllQuestionsAsync();
    }

    public async Task<Question> GetQuestionByIdAsync(string id)
    {
        Guid guid = Guid.Parse(id);
        return await _questionRepo.GetQuestionByIdAsync(guid);
    }
    [Authorize (Roles = "Admin,Instructor")]
    public async Task<Question> AddQuestionAsync(QuestionDTOs.CreateQuestionDTO createQuestion) 
    {
        var question = new Question
        {
            Text = createQuestion.Text,
            Type = createQuestion.Type,
            Choices = createQuestion.Choices.Select(c => new Choice
            {
                Text = c.Text,
                IsCorrect = c.IsCorrect
            }).ToList(),
            CorrectAnswer = createQuestion.CorrectAnswer
        };
        return await _questionRepo.AddQuestionAsync(question);
        
    }
    public async Task<bool> VerifyAnswerAsync(string questionId, List<string> answer)
    {
        Guid questionGuid = Guid.Parse(questionId);
        var question = await _questionRepo.GetQuestionByIdAsync(questionGuid);
        if (question == null)
        {
            throw new Exception($"Question with ID {questionId} not found.");
        }

        switch (question.Type)
        {
            case Question.QuestionType.SingleChoice:
                if (answer.Count != 1)
                {
                    throw new Exception("Single choice question requires exactly one answer.");
                }
                return VerifySingleChoiceAnswer(question, answer[0]);
                
            case Question.QuestionType.MultipleChoice:
                if (answer.Count < 1)
                {
                    throw new Exception("Multiple choice question requires at least one answer.");
                }
                return VerifyMultipleChoiceAnswer(question, answer);
                
            case Question.QuestionType.TrueFalse:
                if (answer.Count != 1)
                {
                    throw new Exception("True/False question requires exactly one answer.");
                }
                return VerifyTrueFalseAnswer(question, answer[0]);
                
            default:
                throw new Exception("Unknown question type.");
        }



    }
    
    private bool VerifySingleChoiceAnswer(Question question, string answer)
    {
        var correctChoice = question.Choices.FirstOrDefault(c => c.IsCorrect);
        if (correctChoice == null)
        {
            throw new Exception("No correct choice found");
        }
        return correctChoice.Text.Equals(answer, StringComparison.OrdinalIgnoreCase);
    }
    
    private bool VerifyMultipleChoiceAnswer(Question question, List<string> answers)
    {
        var correctChoices = question.Choices.Where(c => c.IsCorrect).Select(c => c.Text).ToHashSet();
        var correctSubmittedAnswers = answers.Count(a => correctChoices.Contains(a.Trim()));
        var percentageCorrect = (double)correctSubmittedAnswers / correctChoices.Count;
        return percentageCorrect >= 0.5; // Return true if at least 50% are correct
    }
    
    private bool VerifyTrueFalseAnswer(Question question, string answer)
    {
        if (question.CorrectAnswer == null)
        {
            throw new Exception("Question is not a True/False type");
        }
        bool parsedAnswer;
        if (!bool.TryParse(answer, out parsedAnswer))
        {
            throw new Exception("Invalid answer format for True/False question");
        }
        return question.CorrectAnswer == parsedAnswer;
    }
    
    
    
    
    

    /*public async Task UpdateQuestionAsync(Question question)
    {
        await _questionRepo.UpdateQuestionAsync(question);
    }*/

    /*
    public async Task DeleteQuestionAsync(int id)
    {
        await _questionRepo.DeleteQuestionAsync(id);
    }
    */
    
    
}