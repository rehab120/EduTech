using EduTech.DTO;
using EduTech.Models;
using EduTech.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuizController : ControllerBase
    {
        private readonly IUserQuizRepository _userQuizRepository;

        public UserQuizController(IUserQuizRepository userQuizRepository)
        {
            _userQuizRepository = userQuizRepository;
        }

        [Authorize]
        [HttpGet("TakenQuizzes")]
        public async Task<IActionResult> GetUserTakenQuizzes()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing token.");

            var result = await _userQuizRepository.GetTakenQuizzesWithScoresAsync(userId);

            if (result == null || result.Count == 0)
                return NotFound("No quizzes found for this user.");

            return Ok(result);
        }
        [Authorize]
        [HttpPost("AddTakenQuiz")]
        public async Task<IActionResult> AddTakenQuiz([FromBody] AddUserQuizDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing token.");

            if (string.IsNullOrEmpty(dto.QuizId) || dto.Score < 0)
                return BadRequest("Quiz ID and a valid score are required.");

            var userQuiz = new UserQuiz
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = userId,
                QuizId = dto.QuizId,
                Score = dto.Score
            };

            await _userQuizRepository.AddOrUpdateUserQuizAsync(userQuiz);

            return Ok("Quiz result recorded successfully.");
        }

        [Authorize]
        [HttpGet("QuizScore")]
        public async Task<IActionResult> GetUserQuizScore([FromQuery] string category, [FromQuery] string level)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(level))
                return BadRequest("Category and level are required.");

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid or missing token.");

            var score = await _userQuizRepository.GetUserScoreForQuizAsync(userId, category, level);

            if (score == null)
                return NotFound("User has not taken this quiz.");

            return Ok(new { Score = score });
        }


    }
}
