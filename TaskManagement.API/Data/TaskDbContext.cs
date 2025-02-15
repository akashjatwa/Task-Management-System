using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data.Entities;
using TaskManagement.API.Models;

namespace TaskManagement.API.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Task-User Relationship
         /*   modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // If a user is deleted, delete their tasks too
*/
            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.TaskStatus)
                .HasConversion<string>();
        }
    }

}
