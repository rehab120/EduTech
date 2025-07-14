using System.Security.Principal;
using EduTech.DTO;
using EduTech.IRepositry;
using EduTech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityRepositry identityRepositry;
        private readonly IStudentRepositry studentRepositry;
        public AccountController(IIdentityRepositry identityRepositry, IStudentRepositry studentRepositry)
        {
            this.identityRepositry = identityRepositry;
            this.studentRepositry = studentRepositry;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            if (user == null)
            {
                return BadRequest("Request body is null or invalid. Make sure you're sending valid JSON.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user1 = new Student();
            {
               
                user1.UserName = user.Username;
                user1.Email = user.Email;
                user1.Password = user.Password;
            }

            var (success, errors, CreateUser) = await identityRepositry.RegisterAsync(user1);

            if (!success || CreateUser == null)
            {
                return BadRequest(new { Errors = errors });
            }

            await studentRepositry.AddStudentAsync(CreateUser);
            return Ok("registered successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto login)
        {
            var result = await identityRepositry.LoginAsync(login.Email, login.Password);
            if (result.Success)
            {
                return Ok(new { tokens = result.Token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var result = await identityRepositry.LogoutAsync(token);
            if (!result.Success)
            {
                return BadRequest(new { result.Errors });
            }
            return Ok(new { message = "Logged out successfully" });
        }
        

    }
}
