using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class LoginRequestDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
