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

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto login)
        {
            var result = await identityRepositry.LoginAsync(login.UserName, login.Password);
            if (result.Success)
            {
                return Ok(new { tokens = result.Token, roles = result.Roles });
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
