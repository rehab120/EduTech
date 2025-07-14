using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduTech.Models
{
    public class UserQuiz
    {
        public string Id { get; set; } = string.Empty;

        public string? QuizId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(QuizId))]
        [JsonIgnore]
        public Quiz? Quiz { get; set; }

        public string? StudentId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        [JsonIgnore]
        public Student? Student { get; set; }

        public int Score { get; set; }


    }
}
