using GenerativeAI;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Services.AIService;

public class AIService
{
    private readonly string? apiKey;
    private readonly GoogleAi googleAi;

    public AIService()
    {
        apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        googleAi = new GoogleAi(apiKey);
        
    }
    public async Task<AnswerDTOs.ShortAnswerDTO> VerifyAnswer(Question question, string submittedAnswer)
    {
        var model = googleAi.CreateGenerativeModel("models/gemini-1.5-flash");
        var prompt = $"Question: {question.Text}\nCorrect Answer: {question.CorrectAnswer}\nSubmitted Answer: {submittedAnswer}\nIs the submitted answer correct? Answer with 'true' or 'false' with explanation.";
        var response = await model.GenerateObjectAsync<AnswerDTOs.ShortAnswerDTO>(prompt);
        Console.WriteLine($"AI Response: {response.IsCorrect}, Confidence: {response.Confidence}, explanation: {response.explanation}");
        return response;



    }
}