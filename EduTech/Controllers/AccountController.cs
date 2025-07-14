using System.Security.Principal;
using EduTech.DTO;
using EduTech.IRepositry;
using EduTech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityRepositry identityRepositry;
        public AccountController(IIdentityRepositry identityRepositry)
        {
            this.identityRepositry = identityRepositry;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewAdmin(RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = new Student
            {
                
                UserName = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            var (success, errors, CreateUser) = await identityRepositry.RegisterAsync(student);
            if (!success || CreateUser == null)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok("registered successfully");
        }
    }
}
