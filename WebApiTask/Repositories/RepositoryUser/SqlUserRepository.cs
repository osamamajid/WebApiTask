using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApiTask.data;
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly UserDbContext dbContext;
        public SqlUserRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Create(User user)
        {
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return User;


        }

        public Task<User> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }



        public async Task<List<User>> GetAllTaskUser()
        {
            return await dbContext.TaskUsers.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int IdTask)
        {
            return await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
        }








        public async Task<User> DeleteAsync(int IdTask)
        {
            var exitstingTaskUser = await dbContext.TaskUsers.FirstOrDefaultAsync(x => x.IdTask == IdTask);
            if (exitstingTaskUser != null)
            {
                return null;
            }
            dbContext.TaskUsers.Remove(exitstingTaskUser);
            await dbContext.SaveChangesAsync();
            return exitstingTaskUser;
        }

        public async Task<User> UpdateAsync(int IdTask, UpdataTaskRequestDto updataTaskRequestDto)
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
