using System.ComponentModel.DataAnnotations;

namespace EduTech.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        [Required]
        [DataType(DataType.Password)]


        public string Password { get; set; }

        
        public string? University { get; set; }

        
        public int ?Level { get; set; }




    }
}
