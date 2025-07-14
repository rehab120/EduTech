using EduTech.Repositories;
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

        [HttpGet("TakenQuizzes")]
        public async Task<IActionResult> GetUserTakenQuizzes([FromQuery] string userId)
        {
            var result = await _userQuizRepository.GetTakenQuizzesWithScoresAsync(userId);

            if (result == null || result.Count == 0)
                return NotFound("No quizzes found for this user.");

            return Ok(result);
        }
    }
}
