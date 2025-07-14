using EduTech.Models;
using EduTech.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace EduTech.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ContextEduTech _context;

        public QuizRepository(ContextEduTech context)
        {
            _context = context;
        }

        public async Task<Quiz?> GetQuizByCategoryAndLevelAsync(string category, string level)
        {
            return await _context.Quiz
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Category == category && q.Level == level);
        }
        public void AddQuizWithQuestions(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            _context.SaveChanges();
        }

    }
}
