
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskUser>> GetAllTaskUser();
        Task<TaskUser> GetByIdAsync(int IdTask);
        Task<TaskUser> CreateAsync(TaskUser taskUser);
   
        Task<TaskUser> UpdateAsync(int idTask, UpdataTaskRequestDto updataTaskRequestDto);
        Task<TaskUser> DeleteAsync(int idTask);
    }
} 
 