using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class RegisterRequestDto
    {
       
        public   string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public   string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public   string Password { get; set; }
        public   string [] Roles { get; set; }


    }
}
