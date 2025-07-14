using EduTech.Models;
using EduTech.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EduTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet("GetQuiz")]
        public async Task<IActionResult> GetQuiz([FromQuery] string category, [FromQuery] string level)
        {
            var quiz = await _quizRepository.GetQuizByCategoryAndLevelAsync(category, level);

            if (quiz == null)
                return NotFound("Quiz not found.");

            return Ok(quiz);
        }
    }
}
