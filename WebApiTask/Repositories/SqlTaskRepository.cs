using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Repositories
{
    public class SqlTaskRepository : ITaskRepository
    {
        private readonly UserDbContext dbContext;
        public SqlTaskRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TaskUser> Create(TaskUser taskUser)
        {
            await dbContext.TaskUsers.AddAsync(taskUser);
            await dbContext.SaveChangesAsync();
            return taskUser;


                }

        public Task<TaskUser> CreateAsync(TaskUser taskUser)
        {
            throw new NotImplementedException();
        }

    

        public async Task<List<TaskUser>> GetAllTaskUser()
        {
            return await   dbContext.TaskUsers.ToListAsync();
        }

        public async Task<TaskUser?> GetByIdAsync(int IdTask)
        {
            return await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
        }

        

        
    

        

        public  async Task<TaskUser> DeleteAsync(int IdTask)
        {
            var exitstingTaskUser = await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
            if(exitstingTaskUser != null)
            {
                return null;
            }
            dbContext.TaskUsers.Remove(exitstingTaskUser);
            await dbContext.SaveChangesAsync();
            return exitstingTaskUser;
        }

        public async Task<TaskUser> UpdateAsync(int IdTask, UpdataTaskRequestDto updataTaskRequestDto)
        {
            var exitstingTaskUser = await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
            if (exitstingTaskUser != null)
            {
                return null;
            }
         
            exitstingTaskUser.TaskTitle = updataTaskRequestDto.TaskTitle;
            exitstingTaskUser.TaskDetails = updataTaskRequestDto.TaskDetails;
            exitstingTaskUser.AudioUrl = updataTaskRequestDto.AudioUrl;
            exitstingTaskUser.ImageUrl = updataTaskRequestDto.ImageUrl;
            exitstingTaskUser.VideoUrl = updataTaskRequestDto.VideoUrl;
            exitstingTaskUser.IsCompleted = updataTaskRequestDto.IsCompleted;
            exitstingTaskUser.Created = DateTime.Now;
            exitstingTaskUser.Updated = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return exitstingTaskUser;
        }
    }
}
