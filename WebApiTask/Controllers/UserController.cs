using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
       
        public IActionResult GetAll()
        {
            // جلب البيانات من قاعدة البيانات وتحويلها إلى    Dto
            var userDto = dbContext.Users
                .Select(user => new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password

                })
                .ToList();

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpGet("{UserId:int}")]
         
        public IActionResult GetById([FromRoute] int UserId)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
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
        public IActionResult Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // تحويل DTO إلى نموذج Domin
            var user = new User
            {
                UserName = addUserRequestDto.UserName,
                Email = addUserRequestDto.Email,
                Password = addUserRequestDto.Password

            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] int UserId, [FromBody] UpdateUserRequestDto updateUserRequestDto)

        {

            var userDominModel = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);

            if (userDominModel == null)
            {
                return NotFound();
            }
            userDominModel.UserName = updateUserRequestDto.UserName;
            userDominModel.Email = updateUserRequestDto.Email;
            userDominModel.Password = updateUserRequestDto.Password;    

            dbContext.SaveChanges();
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
        public IActionResult Delete([FromRoute] int UserId)


        {
            var userDominModel = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (userDominModel == null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(userDominModel);
            dbContext.SaveChanges();

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
