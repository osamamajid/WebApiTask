using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTask.Models.Dto;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

        }
        [HttpPost]
        [Route("Register")]
    
        public async Task < IActionResult> RegisteUser([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                PasswordHash = registerRequestDto.Password,
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                //add role to user 
                if (registerRequestDto != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("User wass registered! Please Login");
                }



               
            }
            return BadRequest(" Something went Wrong ");
        }

         
    }
}