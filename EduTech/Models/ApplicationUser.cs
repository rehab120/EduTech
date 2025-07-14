using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EduTech.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

    }
}
