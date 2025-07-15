using System.ComponentModel.DataAnnotations;

namespace EduTech.DTO
{
    public class AddUserQuizDto
    {
        public string QuizId { get; set; }
        public int Score { get; set; }
    }
}
