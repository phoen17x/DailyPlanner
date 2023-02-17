using DailyPlanner.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Context;

/// <summary>
/// Represents the database context.
/// </summary>
public class DailyPlannerContext : DbContext
{
    public DbSet<Notebook> Notebooks { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }

    public DailyPlannerContext(DbContextOptions<DailyPlannerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Notebook>().ToTable("authors");
        modelBuilder.Entity<Notebook>().Property(notebook => notebook.Title).IsRequired();
        modelBuilder.Entity<Notebook>().Property(notebook => notebook.Title).HasMaxLength(50);

        modelBuilder.Entity<TodoTask>().ToTable("tasks");
        modelBuilder.Entity<TodoTask>().Property(task => task.Title).IsRequired();
        modelBuilder.Entity<TodoTask>().Property(task => task.Title).HasMaxLength(50);
        modelBuilder.Entity<TodoTask>().Property(task => task.Description).HasMaxLength(200);
        modelBuilder.Entity<TodoTask>()
            .HasOne(task => task.Notebook)
            .WithMany(notebook => notebook.TodoTasks)
            .HasForeignKey(task => task.NotebookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}