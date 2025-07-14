using EduTech.Models;

namespace EduTech.IRepositry
{
    public interface IStudentRepositry
    {
        Task AddStudentAsync(ApplicationUser user);
        Task<List<Student>> GetAllStudentsAsync();
    }
}
