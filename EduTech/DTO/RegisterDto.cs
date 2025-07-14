using System.ComponentModel.DataAnnotations;

namespace EduTech.DTO
{
    public class RegisterDto
    {
        public String Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
