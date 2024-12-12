using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTask.CustomActionFilers;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    [Authorize]
    public class TaskUserController : ControllerBase
    {
        private readonly UserDbContext dbContext;

        public TaskUserController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllTaskUser()
        { 

            
            var taskuser = dbContext.TaskUsers.ToList();//get data from data base
                                                      
            var taskDto = new List<TaskUserDto>();  // Map Model Domin to DTo
            foreach (var taskUser in taskuser)
            {
                taskDto.Add(new TaskUserDto()

                {
                     IdTask =taskUser.IdTask,
                     AudioUrl =taskUser.AudioUrl,   
                     Created  =  DateTime.Now,
                     FileUrl = taskUser.FileUrl,
                     ImageUrl = taskUser.ImageUrl,
                     IsCompleted = taskUser.IsCompleted,
                     TaskDetails = taskUser.TaskDetails,
                     TaskTitle = taskUser.TaskTitle,
                     Updated = DateTime.Now,
                      VideoUrl = taskUser.VideoUrl, 
                     

                }
                    );

                
                
                }
            

            //return dto
            return Ok(taskuser);
             
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("{IdTask:int}")]
      
        public IActionResult GetId([FromRoute] int IdTask)
        {
            var taskuser = dbContext.TaskUsers.FirstOrDefault(x => x.IdTask == IdTask);
            if (taskuser == null)
            {
                return NotFound();

                  }
            // map /convert  model domin to dto

            var taskDto = new TaskUserDto
            {
                IdTask= taskuser.IdTask,
                AudioUrl=taskuser.AudioUrl,
                Created = DateTime.Now,
                FileUrl = taskuser.FileUrl, 
                ImageUrl = taskuser.ImageUrl,   
                IsCompleted = taskuser.IsCompleted,
                TaskDetails = taskuser.TaskDetails,
                TaskTitle = taskuser.TaskTitle,
                Updated = DateTime.Now,
                 VideoUrl=taskuser.VideoUrl,
                
 
            };
            return Ok(taskDto); // return Dto
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("{IdTask:int}")]
        [ValidateModel]
        public IActionResult Update([FromRoute] int IdTask, UpdataTaskRequest updataTaskRequest)

        {
            var taskuser = dbContext.TaskUsers.FirstOrDefault(x => x.IdTask == IdTask);
            if (taskuser == null)
            {
                return NotFound();

            }
                     

            taskuser.TaskTitle = updataTaskRequest.TaskTitle;
            taskuser.TaskDetails = updataTaskRequest.TaskDetails;
            taskuser.ImageUrl = updataTaskRequest.ImageUrl;
            taskuser.AudioUrl = updataTaskRequest.AudioUrl;
            taskuser.VideoUrl = updataTaskRequest.VideoUrl;
            taskuser.Updated = DateTime.Now;
            dbContext.SaveChanges();

            var userTaskDto = new TaskUserDto
            {
                TaskTitle = taskuser.TaskTitle,
                TaskDetails = taskuser.TaskDetails,
                ImageUrl = taskuser.ImageUrl,
                AudioUrl = taskuser.AudioUrl,
                VideoUrl = taskuser.VideoUrl,
                Updated = taskuser.Updated
            };

            return Ok(userTaskDto);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateModel]
        public IActionResult Create( int IdTask ,[FromBody] AddTaskUserRequestDto addTaskUserRequestDto)  
        {
            // تحويل DTO إلى نموذج Domin
            var userTask = new TaskUser
            {
                TaskTitle = addTaskUserRequestDto.TaskTitle,
                TaskDetails = addTaskUserRequestDto.TaskDetails,
                Created = addTaskUserRequestDto.Created ?? DateTime.Now,  
                Updated = addTaskUserRequestDto.Updated ?? DateTime.Now,  
                IsCompleted = addTaskUserRequestDto.IsCompleted,
                UserId = addTaskUserRequestDto.UserId,
                ImageUrl = addTaskUserRequestDto.ImageUrl,
                AudioUrl = addTaskUserRequestDto.AudioUrl,
                VideoUrl = addTaskUserRequestDto.VideoUrl,
                FileUrl = addTaskUserRequestDto.FileUrl
            };

            // إضافة Task إلى قاعدة البيانات
            dbContext.TaskUsers.Add(userTask);
            dbContext.SaveChanges();

            // تحويل  domin model النموذج إلى Dto
            var userTaskDto = new TaskUserDto
            {
                TaskTitle = userTask.TaskTitle,
                TaskDetails = userTask.TaskDetails,
                ImageUrl = userTask.ImageUrl,
                AudioUrl = userTask.AudioUrl,
                VideoUrl = userTask.VideoUrl,
                Updated = userTask.Updated,
                Created = userTask.Created  
            };              

            
            return CreatedAtAction(nameof(GetId), new { IdTask = userTask.IdTask }, userTaskDto);
        }



        [HttpDelete]
        [Route("{IdTask:int}")]
        [ValidateModel]
        public IActionResult DeleteTask([FromBody] int IdTask)

        {
            var taskuser = dbContext.TaskUsers.SingleOrDefault(x => x.IdTask == IdTask);


            if (taskuser == null)
            {
                return NotFound();
            }

            //delete region
            dbContext.TaskUsers.Remove(taskuser);
            dbContext.SaveChanges();

            // retuer  delete task back


            // map model domin to dtro

            var taskuserDto = new TaskUserDto
            {
                TaskTitle = taskuser.TaskTitle,
                TaskDetails = taskuser.TaskDetails,
                ImageUrl = taskuser.ImageUrl,
                FileUrl = taskuser.FileUrl,
                AudioUrl = taskuser.AudioUrl,
                VideoUrl = taskuser.VideoUrl,
                IsCompleted = taskuser.IsCompleted,
                Updated = taskuser.Updated,
                IdTask = taskuser.IdTask,
                Created = taskuser.Created




            };

            return Ok(taskuserDto); 
               
        }
    }




}