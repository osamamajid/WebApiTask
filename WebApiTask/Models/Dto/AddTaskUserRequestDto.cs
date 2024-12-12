using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class AddTaskUserRequestDto
    {
        public int IdTask { get; set; } 
        public string? TaskTitle { get; set; }
        public string? TaskDetails { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;  
        public DateTime? Updated { get; set; } 
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

        public string? ImageUrl { get; set; }
        public string? FileUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }
    }
}
