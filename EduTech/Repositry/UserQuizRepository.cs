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
        public async Task AddOrUpdateUserQuizAsync(UserQuiz userQuiz)
        {
            var existing = await _context.UserQuiz
                .FirstOrDefaultAsync(uq => uq.QuizId == userQuiz.QuizId && uq.StudentId == userQuiz.StudentId);

            if (existing != null)
            {
                existing.Score = userQuiz.Score;
                _context.UserQuiz.Update(existing);
            }
            else
            {
                await _context.UserQuiz.AddAsync(userQuiz);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<int?> GetUserScoreForQuizAsync(string studentId, string category, string level)
        {
            return await _context.UserQuiz
                .Include(uq => uq.Quiz)
                .Where(uq => uq.StudentId == studentId && uq.Quiz.Category == category && uq.Quiz.Level == level)
                .Select(uq => (int?)uq.Score)
                .FirstOrDefaultAsync();
        }


    }
}
