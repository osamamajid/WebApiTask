using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTask.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
     
         
        public string? UserName { get; set; }
            
        public string? Email { get; set; }
       public string? Password { get; set; }

       // public ICollection<TaskUser>? TaskUsers { get; set; }
    }
}
  