using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Repositories.QuestionRepo;
using OnlineQuizSystem.Services.AIService;

namespace OnlineQuizSystem.Services.QuestionService;

public class QuestionService (IQuestionRepo _questionRepo , IAIService _aiService): IQuestionService
{
    public async Task<IEnumerable<QuestionDTOs.QuestionResponseDTO>> GetAllQuestionsAsync()
    {
        return await _questionRepo.GetAllQuestionsAsync();
    }

    public async Task<QuestionDTOs.QuestionResponseDTO?> GetQuestionByIdAsync(string id)
    {
        Guid guid = Guid.Parse(id);
        return await _questionRepo.GetQuestionByIdAsync(guid);
    }
    [Authorize (Roles = "Admin,Instructor")]
    public async Task<Question> AddQuestionAsync(QuestionDTOs.QuestionDTO createQuestion) 
    {
        validate(createQuestion);
        // Map DTO to Entity 
        var question = new Question
        {
            Text = createQuestion.Text,
            Type = createQuestion.Type,
            Choices = createQuestion.Choices.Select(c => new Choice
            {
                Text = c.Text,
                IsCorrect = c.IsCorrect
            }).ToList(),
            CorrectAnswer = createQuestion.CorrectAnswer,
            Answer = createQuestion.Answer,
            Points = createQuestion.Points,
            ImageUrl = createQuestion.ImageURL,
            CategoryId = createQuestion.CategoryId
        };
        return await _questionRepo.AddQuestionAsync(question);
        
    }
    public async Task<Question> UpdateQuestionAsync(Guid id,QuestionDTOs.QuestionDTO questionDto)
    {
        validate(questionDto);
        var question = new Question
        {
            Text = questionDto.Text,
            Type = questionDto.Type,
            Choices = questionDto.Choices.Select(c => new Choice
            {
                Text = c.Text,
                IsCorrect = c.IsCorrect
            }).ToList(),
            CorrectAnswer = questionDto.CorrectAnswer,
            Answer = questionDto.Answer,
            Points = questionDto.Points,
            ImageUrl = questionDto.ImageURL,
            CategoryId = questionDto.CategoryId
        };
        var existingQuestion = await _questionRepo.GetQuestionEntityByIdAsync(id);
        if (existingQuestion == null)
            throw new Exception("Question not found.");
        
        // update fields
        existingQuestion.Text = question.Text;
        existingQuestion.Type = question.Type;
        existingQuestion.Choices = question.Choices;
        existingQuestion.CorrectAnswer = question.CorrectAnswer;
        existingQuestion.Answer = question.Answer;
        existingQuestion.Points = question.Points;
        existingQuestion.ImageUrl = question.ImageUrl;
        existingQuestion.CategoryId = question.CategoryId;
        
            
            
       
        return await _questionRepo.UpdateQuestionAsync(id, question);




    }

    public async Task DeleteQuestionAsync(Guid id)
    {
        await _questionRepo.DeleteQuestionAsync(id);
    }

    public void validate(QuestionDTOs.QuestionDTO dto)
    {
        // all exception must be reformed to normal exception rather than HTTP exceptions
        switch (dto.Type)
        {
            case Question.QuestionType.SingleChoice:
            case Question.QuestionType.MultipleChoice:
                if (dto.Choices == null || dto.Choices.Count == 0)
                {
                    throw new Exception("Choices cannot be null or empty for Single and Multiple Choice questions.");
                }

                // nullify CorrectAnswer and Answer for these types
                dto.CorrectAnswer = null;
                dto.Answer = null;
                break;
            case Question.QuestionType.TrueFalse:
                if (dto.CorrectAnswer == null)
                {
                    throw new Exception("CorrectAnswer must be provided for True/False questions.");
                }

                // nullify Choices and Answer for this type
                dto.Choices = [];
                dto.Answer = null;
                break;
            case Question.QuestionType.ShortAnswer:
                if (string.IsNullOrWhiteSpace(dto.Answer))
                {
                    throw new Exception("Answer cannot be null or empty for Short Answer questions.");
                }

                // nullify Choices and CorrectAnswer for this type
                dto.Choices = [];
                dto.CorrectAnswer = null;
                break;
            default:
                throw new Exception("Invalid question type.");
        }
    }
    
    

    public async Task<bool> VerifyAnswerAsync(string questionId, List<string> answer)
    {
        Guid questionGuid = Guid.Parse(questionId);
        var question = await _questionRepo.GetQuestionEntityByIdAsync(questionGuid);
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
            case Question.QuestionType.ShortAnswer:
                if (answer.Count != 1)
                {
                    throw new Exception("Short answer question requires exactly one answer.");
                }

                return VerifyShortAnswer(question, answer[0]);

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
    
    private bool VerifyShortAnswer(Question question, string answer)
    {
        if (string.IsNullOrWhiteSpace(question.Answer))
        {
            throw new Exception("No correct answer provided for short answer question");
        }
        if (string.Equals(question.Answer.Trim(), answer.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            return true; // Direct match
        }
        var verifiedAnswer = _aiService.VerifyAnswer(question, answer).Result;
        return verifiedAnswer.IsCorrect;
        
    }
    
    
    
    

  
    
}