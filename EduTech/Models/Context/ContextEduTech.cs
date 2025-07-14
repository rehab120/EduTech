using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduTech.Models.Context
{
    public class ContextEduTech : IdentityDbContext<ApplicationUser>
    {
        public ContextEduTech() : base() { }
        public ContextEduTech(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<UserQuiz> UserQuiz { get; set; }

    }
}
