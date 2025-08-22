using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Services.QuestionService;

namespace OnlineQuizSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController (IQuestionService _QuestionService) : Controller
{
    /*// This controller will handle all the question-related operations
    // such as adding, updating, deleting, and fetching questions.
    // adding questions, updating questions, deleting questions will be performed by the admin/later privileged user roles
    // fetching questions will be performed by the user
    
    [HttpGet]
    public async Task<IActionResult> GetQuestions()
    {
        var Questions = await _QuestionService.GetAllQuestions();
        if (Questions == null || !Questions.Any())
            return NotFound("No questions found.");
        return Ok(Questions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(int id)
    {
        var Question = await _QuestionService.GetQuestionById(id);
        if (Question == null)
            return NotFound($"Question not found.");
        return Ok(Question);
    }
    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> AddQuestion([FromBody] DTOs.QuestionDTOs.AddQuestionDTO AddQuestionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid question data.");
        
        try
        {
            var Question = await _QuestionService.AddQuestionAsync(AddQuestionDTO);
            return CreatedAtAction(nameof(GetQuestionById), new { id = Question.Id }, Question);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // verify answer for testing purposes before building the quiz
    [HttpPost("{id}")]
    public async Task<IActionResult> VerifyAnswer(int id, [FromBody] DTOs.QuestionDTOs.VerifyAnswerDTO VerifyAnswerDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid answer data.");
        
        try
        {
            var isCorrect = await _QuestionService.VerifyAnswerAsync(id, VerifyAnswerDTO);
            return Ok(new { IsCorrect = isCorrect });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }*/
    }


