using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTask.CustomActionFilers;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;
using WebApiTask.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    [Authorize]
    public class TaskUserController : ControllerBase
    {
        private readonly UserDbContext dbContext;
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;

        public TaskUserController(UserDbContext dbContext, ITaskRepository taskRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.taskRepository = taskRepository;
            this.mapper=mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public  async Task<IActionResult> GetAllTaskUser()
        { 

            
            var taskuser = await  taskRepository.GetAllTaskUser();//get data from data base
                                                      
            //var taskDto = new List<TaskUserDto>();  // Map Model Domin to DTo 
            //foreach (var taskUser in taskuser)
            //{
            //    taskDto.Add(new TaskUserDto()

            //    { 
            //         IdTask =taskUser.IdTask,
            //         AudioUrl =taskUser.AudioUrl,   
            //         Created  =  DateTime.Now,
            //         FileUrl = taskUser.FileUrl,
            //         ImageUrl = taskUser.ImageUrl,
            //         IsCompleted = taskUser.IsCompleted,
            //         TaskDetails = taskUser.TaskDetails,
            //         TaskTitle = taskUser.TaskTitle,
            //         Updated = DateTime.Now,
            //          VideoUrl = taskUser.VideoUrl, 
                     

            //    }
            //        );

                
                
            //    }
            
            //return dto
            return Ok(mapper.Map<List<TaskUserDto>>(taskuser));
             
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("{IdTask:int}")]

        public async Task <IActionResult> GetByIdAsync([FromRoute] int IdTask)

        {
            var taskuser = await taskRepository.GetByIdAsync(IdTask);
            if (taskuser == null)
            {
                return NotFound();

                  }
            // map /convert  model domin to dto
            
            //var taskDto = new TaskUserDto
            //{
            //    IdTask= taskuser.IdTask,
            //    AudioUrl=taskuser.AudioUrl,
            //    Created = DateTime.Now,
            //    FileUrl = taskuser.FileUrl, 
            //    ImageUrl = taskuser.ImageUrl,   
            //    IsCompleted = taskuser.IsCompleted,
            //    TaskDetails = taskuser.TaskDetails,
            //    TaskTitle = taskuser.TaskTitle,
            //    Updated = DateTime.Now,
            //     VideoUrl=taskuser.VideoUrl,
                
 
            //};
            return Ok(mapper.Map<TaskUserDto>(taskuser)); // return Dto
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("{IdTask:int}")]
        [ValidateModel]
        public async Task <IActionResult> UpdateAsync([FromRoute] int IdTask, UpdataTaskRequestDto updataTaskRequestDto)

        {
            var taskuser =mapper.Map<TaskUser>(updataTaskRequestDto);

            //var taskuser = new TaskUser

            //{
            //    TaskTitle = updataTaskRequestDto.TaskTitle,
            //    TaskDetails = updataTaskRequestDto.TaskDetails,
            //    ImageUrl = updataTaskRequestDto.ImageUrl,
            //    AudioUrl = updataTaskRequestDto.AudioUrl,
            //    VideoUrl = updataTaskRequestDto.VideoUrl,
            //    Updated = DateTime.Now,
          
            //};

            taskuser = await taskRepository.UpdateAsync(IdTask, updataTaskRequestDto);
                if (taskuser == null) 
            {
                return NotFound();

            }


     return Ok (mapper.Map<TaskUserDto>(taskuser));


            //    new TaskUserDto
            //{
            //    TaskTitle = taskuser.TaskTitle,
            //    TaskDetails = taskuser.TaskDetails,
            //    ImageUrl = taskuser.ImageUrl,
            //    AudioUrl = taskuser.AudioUrl,
            //    VideoUrl = taskuser.VideoUrl,
            //    Updated = taskuser.Updated
            //});
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync( int IdTask ,[FromBody] AddTaskUserRequestDto addTaskUserRequestDto)  
        {

           var taskuserModel =mapper.Map<TaskUser>(addTaskUserRequestDto);
            // تحويل DTO إلى نموذج Domin
            //var userTask = new TaskUser
            //{
            //    TaskTitle = addTaskUserRequestDto.TaskTitle,
            //    TaskDetails = addTaskUserRequestDto.TaskDetails,
            //    Created = addTaskUserRequestDto.Created ?? DateTime.Now,  
            //    Updated = addTaskUserRequestDto.Updated ?? DateTime.Now,  
            //    IsCompleted = addTaskUserRequestDto.IsCompleted,
            //    UserId = addTaskUserRequestDto.UserId,
            //    ImageUrl = addTaskUserRequestDto.ImageUrl,
            //    AudioUrl = addTaskUserRequestDto.AudioUrl,
            //    VideoUrl = addTaskUserRequestDto.VideoUrl,
            //    FileUrl = addTaskUserRequestDto.FileUrl
            //};

            // إضافة Task إلى قاعدة البيانات
            taskuserModel = await  taskRepository.CreateAsync(taskuserModel);

            var taskuserDto = mapper.Map<TaskUser>(taskuserModel);
            await dbContext.SaveChangesAsync();

            // تحويل  domin model النموذج إلى Dto
            //var userTaskDto = new TaskUserDto
            //{
            //    TaskTitle = userTask.TaskTitle,
            //    TaskDetails = userTask.TaskDetails,
            //    ImageUrl = userTask.ImageUrl,
            //    AudioUrl = userTask.AudioUrl,
            //    VideoUrl = userTask.VideoUrl,
            //    Updated = userTask.Updated,
            //    Created = userTask.Created  
            //};              

            
            return CreatedAtAction(nameof(GetByIdAsync), new { IdTask = taskuserModel.IdTask }, taskuserDto);
        }



        [HttpDelete]
        [Route("{IdTask:int}")]
        [ValidateModel]
        public async Task< IActionResult> DeleteAsync([FromBody] int IdTask)

        {
            var taskuser = await taskRepository.DeleteAsync(IdTask); 


            if (taskuser == null)
            {
                return NotFound();
            }

            //delete region
           
            // retuer  delete task back


            // map model domin to dtro

            //var taskuserDto = new TaskUserDto 
            //{
            //    TaskTitle = taskuser.TaskTitle,
            //    TaskDetails = taskuser.TaskDetails,
            //    ImageUrl = taskuser.ImageUrl,
            //    FileUrl = taskuser.FileUrl,
            //    AudioUrl = taskuser.AudioUrl,
            //    VideoUrl = taskuser.VideoUrl,
            //    IsCompleted = taskuser.IsCompleted,
            //    Updated = taskuser.Updated,
            //    IdTask = taskuser.IdTask,
            //    Created = taskuser.Created




            //};

            return Ok(mapper.Map<TaskUserDto>(taskuser)); 
               
        }
    }




}