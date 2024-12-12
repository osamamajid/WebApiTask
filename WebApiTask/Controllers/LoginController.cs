using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTask.CustomActionFilers;
using WebApiTask.data;
using WebApiTask.Models;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext dbContext;
        public LoginController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] User user)
        {
            var foundUser = dbContext.Users.FirstOrDefault(u => u.UserName == user.Email && u.Password == user.Password);
            
            if (foundUser != null)
                return Ok(foundUser);
            
            return Ok("'Login seccess'" );
        }   

    }
}
