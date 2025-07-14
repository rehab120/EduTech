using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduTech.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public List<Question> Questions { get; set; } = new List<Question>();

        [Required]
        public string Level { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
