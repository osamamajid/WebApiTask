using Microsoft.EntityFrameworkCore;
using WebApiTask.Models;

namespace WebApiTask.data
{
    public class UserDbContext : DbContext
    {
     
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

  
        public DbSet<User> Users { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }

  
         
    }
}
