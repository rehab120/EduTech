using EduTech.Models;

namespace EduTech.Repositories
{
    public interface IQuizRepository
    {
        Task<Quiz?> GetQuizByCategoryAndLevelAsync(string category, string level);
        void AddQuizWithQuestions(Quiz quiz);

    }
}
