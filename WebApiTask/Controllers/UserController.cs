using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTask.CustomActionFilers;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext dbContext;

        public UserController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet]

        public async Task <IActionResult> GetAll()
        {
            // جلب البيانات من قاعدة البيانات وتحويلها إلى    Dto
            var userDto = await dbContext.Users
                .Select(user => new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password

                })
                .ToListAsync();

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpGet("{UserId:int}")]

        public async Task <IActionResult> GetById([FromRoute] int UserId)
        {
            var user =  await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password

            };

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost]
        //  [ValidateModel]
        public async Task < IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // تحويل DTO إلى نموذج Domin
            var user = new User
            {
                UserName = addUserRequestDto.UserName,
                Email = addUserRequestDto.Email,
                Password = addUserRequestDto.Password

            };

          await  dbContext.Users.AddAsync(user);
            dbContext.SaveChangesAsync();

            // تحويل النموذج إلى Dto
            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password

            };


            return CreatedAtAction(nameof(GetById), new { UserId = userDto.UserId }, userDto);
        }
        [AllowAnonymous]
        [HttpPut]
        [Route("{UserId:int}")]
        //[ValidateModel]
        public async Task <IActionResult> Update([FromRoute] int UserId, [FromBody] UpdateUserRequestDto updateUserRequestDto)

        {

            var userDominModel = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (userDominModel == null)
            {
                return NotFound();
            }
            userDominModel.UserName = updateUserRequestDto.UserName;
            userDominModel.Email = updateUserRequestDto.Email;
            userDominModel.Password = updateUserRequestDto.Password;    

            dbContext.SaveChangesAsync();
            var userDto = new User
            {

                UserId = userDominModel.UserId,
                UserName = userDominModel.UserName,
                Email = userDominModel.Email,
                Password = userDominModel.Password
            };

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{UserId:int}")]
        [ValidateModel]
        public async Task <IActionResult> Delete([FromRoute] int UserId)


        {
            var userDominModel = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (userDominModel == null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(userDominModel);
            dbContext.SaveChangesAsync();

            var userDto = new User
            {

                UserId = userDominModel.UserId,
                UserName = userDominModel.UserName,
                Email = userDominModel.Email  ,
                Password = userDominModel.Password
            };



            return Ok(userDto);
        }
    }

}
