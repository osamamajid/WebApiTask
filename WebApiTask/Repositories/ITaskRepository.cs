
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskUser>> GetAllTaskUser();
        Task<TaskUser> GetByIdAsync(int IdTask);
        Task<TaskUser> CreateAsync(TaskUser taskUser);
        Task<TaskUser> UpdateAsync(int IdTask, TaskUser taskUser);
        Task UpdateAsync(int idTask, UpdataTaskRequestDto updataTaskRequestDto);
    }
}
 