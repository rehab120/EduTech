using EduTech.DTO;
using EduTech.Models;

namespace EduTech.Repositories
{
    public interface IUserQuizRepository
    {
        Task<List<UserQuizResultDto>> GetTakenQuizzesWithScoresAsync(string studentId);
    }
}
