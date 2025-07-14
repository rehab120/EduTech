using EduTech.DTO;
using EduTech.Models;
using EduTech.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace EduTech.Repositories
{
    public class UserQuizRepository : IUserQuizRepository
    {
        private readonly ContextEduTech _context;

        public UserQuizRepository(ContextEduTech context)
        {
            _context = context;
        }

        public async Task<List<UserQuizResultDto>> GetTakenQuizzesWithScoresAsync(string studentId)
        {
            return await _context.UserQuiz
                .Where(uq => uq.StudentId == studentId)
                .Include(uq => uq.Quiz)
                .Select(uq => new UserQuizResultDto
                {
                    Category = uq.Quiz.Category,
                    Level = uq.Quiz.Level,
                    Score = uq.Score
                })
                .ToListAsync();
        }
    }
}
