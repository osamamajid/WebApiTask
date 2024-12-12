using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class AddUserRequestDto
    {
        [StringLength(100)]
        public required string UserName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }

       // public ICollection<TaskUser>? TaskUsers { get; set; }

    }
}
