using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody]string role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = role;
                IdentityResult Result = await roleManager.CreateAsync(roleModel);
                if (Result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in Result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }


            }
            return BadRequest(ModelState);
        }
    }
}
