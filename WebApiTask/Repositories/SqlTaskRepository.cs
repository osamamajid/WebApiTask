﻿using Microsoft.EntityFrameworkCore;
using WebApiTask.data;
using WebApiTask.Models;

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

        public async Task<TaskUser?> GetById(int IdTask)
        {
            return await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
        }

        public Task<TaskUser> GetByIdAsync(int IdTask)
        {
            throw new NotImplementedException();
        }

        
        public async Task<TaskUser> UpdateAsync(int IdTask, TaskUser taskUser)
        {
            var exitstingTaskUser = await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
                if (exitstingTaskUser != null)
            {
                return null;
            }
            exitstingTaskUser.IdTask =taskUser.IdTask;
           exitstingTaskUser.TaskTitle = taskUser.TaskTitle;
            exitstingTaskUser.TaskDetails = taskUser.TaskDetails;
            exitstingTaskUser.AudioUrl = taskUser.AudioUrl;
            exitstingTaskUser.ImageUrl = taskUser.ImageUrl;
            exitstingTaskUser.VideoUrl  = taskUser.VideoUrl ;
            exitstingTaskUser.IsCompleted   = taskUser.IsCompleted;
            exitstingTaskUser.Created  = taskUser. Created ;
            exitstingTaskUser.Updated   = taskUser.Updated ;

            await dbContext.SaveChangesAsync();
            return exitstingTaskUser;
        }
    }
}