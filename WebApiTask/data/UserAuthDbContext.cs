using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiTask.data
{
    public class UserAuthDbContext : IdentityDbContext

    {

        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options) : base(options) 

        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "2";
            var writerRoleId = "4";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
            {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName ="Reader".ToUpper()
            },
                     new IdentityRole
            {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName ="Writer".ToUpper()
            }

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
