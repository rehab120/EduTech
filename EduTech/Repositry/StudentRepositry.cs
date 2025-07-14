using EduTech.IRepositry;
using EduTech.Models;
using EduTech.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace EduTech.Repositry
{
    public class StudentRepositry : IStudentRepositry
    {
        private readonly ContextEduTech context;
        public StudentRepositry(ContextEduTech context)
        {
            this.context = context;
        }
        public async Task AddStudentAsync(ApplicationUser user)
        {
            var student = new Student
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash
                
            };
            context.Student.Add(student);
            await context.SaveChangesAsync();
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await context.Student.ToListAsync();
        }
    }
}
