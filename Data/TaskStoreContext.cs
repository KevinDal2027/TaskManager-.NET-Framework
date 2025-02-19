using ASPWebApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPWebApp.Data;

public class TaskStoreContext(DbContextOptions<TaskStoreContext> options) : DbContext(options) {
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Academics" },
            new { Id = 2, Name = "Others" }  
        );
    }
}
