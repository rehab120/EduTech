using System.ComponentModel.DataAnnotations;

namespace EduTech.DTO
{
    public class UserQuizResultDto
    {
        public string Category { get; set; }
        public string Level { get; set; }
        public int Score { get; set; }
    }

}
