using EduTech.ID_Generator;
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
        private readonly IdGenerator idGenerator;

        public QuizController(IQuizRepository quizRepository,IdGenerator idGenerator)
        {
            _quizRepository = quizRepository;
            this.idGenerator = idGenerator;
        }

        [HttpGet("GetQuiz")]
        public async Task<IActionResult> GetQuiz([FromQuery] string category, [FromQuery] string level)
        {
            var quiz = await _quizRepository.GetQuizByCategoryAndLevelAsync(category, level);

            if (quiz == null)
                return NotFound("Quiz not found.");

            return Ok(quiz);
        }
        [HttpPost("AddQuiz")]
        public async Task<IActionResult> AddQuiz([FromBody] Quiz quiz)
        {
            if (quiz == null || quiz.Questions == null || quiz.Questions.Count == 0)
                return BadRequest("Quiz and its questions are required.");

            //quiz.Id = Guid.NewGuid().ToString();
            quiz.Id = Guid.NewGuid().ToString();
            foreach (var q in quiz.Questions)
            {
                q.Id = Guid.NewGuid().ToString();
                q.QuizId = quiz.Id;
            }

            _quizRepository.AddQuizWithQuestions(quiz);
            return Ok("Quiz added successfully.");
        }

    }
}
