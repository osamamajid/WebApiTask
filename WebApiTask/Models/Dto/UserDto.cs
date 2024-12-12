using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTask.Models.Dto
{
    public class UserDto
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [StringLength(100)]
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }

        //public ICollection<TaskUser>? TaskUsers { get; set; }
    }
}
