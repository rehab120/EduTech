using EduTech.DTO;
using EduTech.Models;

namespace EduTech.Repositories
{
    public interface IUserQuizRepository
    {
        Task<List<UserQuizResultDto>> GetTakenQuizzesWithScoresAsync(string studentId);
        Task AddOrUpdateUserQuizAsync(UserQuiz userQuiz);
        Task<int?> GetUserScoreForQuizAsync(string studentId, string category, string level);
    }
}
