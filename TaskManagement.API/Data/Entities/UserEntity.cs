using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Data.Entities
{

    [Table("Users")] // Table name in DB

    public class UserEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; } // Store hashed password

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property: One User can have many Tasks
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
