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
        [HttpGet("TakenQuizzesById")]
        public async Task<IActionResult> GetTakenQuizzesById([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID is required.");

            var result = await _userQuizRepository.GetTakenQuizzesWithScoresAsync(userId);

            if (result == null || result.Count == 0)
                return NotFound("No quizzes found for this user.");

            return Ok(result);
        }

    }
}
