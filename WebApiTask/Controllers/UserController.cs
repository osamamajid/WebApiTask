using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTask.CustomActionFilers;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;
using WebApiTask.Repositories;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext dbContext;
        private readonly IMapper mapper;
        private readonly   IUserRepository userRepository ;


        public UserController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userRepository = userRepository;   

        }

        [AllowAnonymous]
        [HttpGet]

        public async Task <IActionResult> GetAllasync()
        {
            // جلب البيانات من قاعدة البيانات وتحويلها إلى    Dto

        var user = await  userRepository.GetAllasync();
           
            //var userDto = await dbContext.Users
            //    .Select(user => new UserDto
            //    {
            //        UserId = user.UserId,
            //        UserName = user.UserName,
            //        Email = user.Email,
            //        Password = user.Password

            //    })
            //    .ToListAsync();

            return Ok(mapper.Map<List<UserDto>>(user));
        }

        [AllowAnonymous]
        [HttpGet("{UserId:int}")]

        public async Task <IActionResult> GetByIdAsync([FromRoute] int UserId)
        {
            var user =  await userRepository.GetByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            //var userDto = new UserDto
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    Password = user.Password

            //};

            return Ok(mapper.Map<UserDto>(user));  // return dto
        }

        [AllowAnonymous]
        [HttpPost]
        //  [ValidateModel]
        public async Task < IActionResult> CreateAsync([FromBody] AddUserRequestDto addUserRequestDto)
        {
            var user =mapper.Map<User>(addUserRequestDto);
            // تحويل DTO إلى نموذج Domin
            //var user = new User
            //{
            //    UserName = addUserRequestDto.UserName,
            //    Email = addUserRequestDto.Email,
            //    Password = addUserRequestDto.Password

            //};
            user = await userRepository.CreateAsync(user);
           var userDto = mapper.Map<UserDto>(user);

       await     dbContext.SaveChangesAsync();

            //// تحويل النموذج إلى Dto
            //var userDto = new UserDto
            //{
            //    UserId = user.UserId,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    Password = user.Password

            //};


            return CreatedAtAction(nameof(GetByIdAsync), new { UserId = user.UserId }, userDto);
        }
        [AllowAnonymous]
        [HttpPut]
        [Route("{UserId:int}")]
        //[ValidateModel]
        public async Task <IActionResult> UpdateAsync([FromRoute] int UserId, [FromBody] UpdateUserRequestDto updateUserRequestDto)

        {

            var userDominModel = mapper.Map<User>(updateUserRequestDto);
            User user = await userRepository.UpdateAsync(UserId, updateUserRequestDto);

            if (user == null)
            {
                return NotFound();
            }
            //userDominModel.UserName = updateUserRequestDto.UserName;
            //userDominModel.Email = updateUserRequestDto.Email;
            //userDominModel.Password = updateUserRequestDto.Password;    


            //dbContext.SaveChangesAsync();
            //var userDto = new User
            //{

            //    UserId = userDominModel.UserId,
            //    UserName = userDominModel.UserName,
            //    Email = userDominModel.Email,
            //    Password = userDominModel.Password
            //};

            return Ok(mapper.Map<User>(user));
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
