
using WebApiTask.Models;
using WebApiTask.Models.Dto;

namespace WebApiTask.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllasync();
        Task<User> GetByIdAsync(int UserId);
        Task<User> CreateAsync(User user);
   
        Task<User> UpdateAsync(int UseUserId, UpdateUserRequestDto  updateUserRequestDto);
        Task<User> DeleteAsync(int UserId);
    }
} 
 