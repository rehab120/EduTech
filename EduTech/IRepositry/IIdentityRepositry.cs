using EduTech.Models;

namespace EduTech.IRepositry
{
    public interface IIdentityRepositry
    {
        Task<(bool Success, List<string> Errors, ApplicationUser? User)> RegisterAsync(Student user);
        Task<(bool Success, string Token, List<string> Errors, List<string> Roles)> LoginAsync(string Username, String password);
    }
}
