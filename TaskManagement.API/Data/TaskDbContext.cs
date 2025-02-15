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

        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.TaskStatus)
                .HasConversion<string>();
        }
    }

}
