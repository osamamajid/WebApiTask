using System.ComponentModel.DataAnnotations;
using WebApiTask.Models;

namespace WebApiTask.Models
{
    public class TaskUser
    {
        [Key]   
        public int IdTask { get; set; }

        public string? TaskTitle { get; set; }
        public string? TaskDetails { get; set; }
        public DateTime? Created { get; set; } = null;
        public DateTime? Updated { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

      //  public required User User { get; set; }

        public string? ImageUrl { get; set; }
        public string? FileUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }
    }
}
