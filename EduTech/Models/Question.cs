using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduTech.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [ValidateNever]
        [JsonPropertyName("theQuestion")]
        public string TheQuestion { get; set; } = string.Empty;

        [ValidateNever]
        public string CorrectAnswer { get; set; } = string.Empty;

        [ValidateNever]
        public List<string> Options { get; set; } = new List<string>();

        public string? QuizId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(QuizId))]
        [JsonIgnore]
        public Quiz? Quiz { get; set; }


    }
}
