using System.ComponentModel.DataAnnotations;

namespace WebApiTask.Models.Dto
{
    public class UpdataTaskRequest
    {
         

        public string? TaskTitle { get; set; }
        public string? TaskDetails { get; set; }
        
        public DateTime? Updated { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

       

        public string? ImageUrl { get; set; }
        public string? FileUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }
    }

}
 
