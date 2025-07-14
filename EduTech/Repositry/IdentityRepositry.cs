using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduTech.IRepositry;
using EduTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EduTech.Repositry
{
    public class IdentityRepositry : IIdentityRepositry
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        internal static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();
        public IdentityRepositry(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = _userManager;
            this._configuration = _configuration;
            this._roleManager = _roleManager;
            this.signInManager = signInManager;
        }
        public async Task<(bool Success, List<string> Errors, ApplicationUser? User)> RegisterAsync(Student user)
        {
            ApplicationUser newUser = new ApplicationUser();
            {
                
                newUser.UserName = user.UserName;
                newUser.Email = user.Email;

            }
            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToList(), null);
            }
            await _userManager.AddToRoleAsync(newUser, "Student");
            return (true, new List<string>(), newUser);
        }

        public async Task<(bool Success, string Token, List<string> Errors, List<string> Roles)> LoginAsync(string Username, String password)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
                return (false, null, new() { "Invalid Username or password" }, null);

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, null, new() { "Invalid Username or password" }, null);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretKey = _configuration["JWT:SecretKey"];
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException("JWT:SecretKey");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, tokenString, new(), roles.ToList());
        }


    }
}
