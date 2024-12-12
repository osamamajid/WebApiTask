using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models
{
    public class AuthenticateRequestDto
    {

        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
} 