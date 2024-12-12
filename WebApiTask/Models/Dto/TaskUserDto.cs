using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class TaskUserDto
    {
        [Key]
        public int IdTask { get; set; }

        public string? TaskTitle { get; set; }
        public string? TaskDetails { get; set; }
        public DateTime? Created { get; set; } = null;
        public DateTime? Updated { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }

      //  public User User { get; set; }

        public string? ImageUrl { get; set; }
        public string? FileUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }

    }
}
