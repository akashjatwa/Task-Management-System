using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.API.Models;

namespace TaskManagement.API.Data.Entities
{
    [Table("Tasks")] // Table name in DB
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public Status TaskStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(20)]
        public string TaskCode { get; set; } = $"TASK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";


        // Foreign Key for User
      /*  public int UserId { get; set; }
        public UserEntity User { get; set; } // Navigation Property    

*/
    }
}
