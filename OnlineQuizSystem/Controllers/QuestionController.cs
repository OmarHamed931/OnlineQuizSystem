using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.QuestionService;
using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // All endpoints require authentication
public class QuestionController(IQuestionService _QuestionService) : Controller
{
    // This controller will handle all the question-related operations
    // such as adding, updating, deleting, and fetching questions.
    // adding questions, updating questions, deleting questions will be performed by the admin/later privileged user roles
    // this controller endpoints might cease to exist once the quiz functionality is fully implemented as questions will be added while creating the quiz

    [HttpGet]
    public async Task<IActionResult> GetQuestions()
    {
        var Questions = await _QuestionService.GetAllQuestionsAsync();
        if (Questions == null || !Questions.Any())
            return NotFound("No questions found.");
        return Ok(Questions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(string id)
    {
        var Question = await _QuestionService.GetQuestionByIdAsync(id);
        if (Question == null)
            return NotFound($"Question not found.");
        return Ok(Question);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> AddQuestion([FromBody] QuestionDTOs.CreateQuestionDTO CreateQuestionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid question data.");

        try
        {
            var Question = await _QuestionService.AddQuestionAsync(CreateQuestionDTO);
            return CreatedAtAction(nameof(GetQuestionById), new { id = Question.Id }, Question);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // verify answer for testing purposes before building the quiz
    [HttpPost("{id}")]
    public async Task<IActionResult> VerifyAnswer(string id, List<string> answers)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid answer data.");

        try
        {
            var isCorrect = await _QuestionService.VerifyAnswerAsync(id, answers);
            return Ok(new { IsCorrect = isCorrect });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


