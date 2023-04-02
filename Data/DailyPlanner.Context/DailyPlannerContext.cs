using DailyPlanner.Context.Entities;
using DailyPlanner.Context.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Context;

/// <summary>
/// Represents the database context.
/// </summary>
public class DailyPlannerContext : IdentityDbContext<User, UserRole, Guid>
{
    public DbSet<Notebook> Notebooks { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }

    public DailyPlannerContext(DbContextOptions<DailyPlannerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<UserRole>().ToTable("roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        modelBuilder.Entity<Notebook>().ToTable("notebooks");
        modelBuilder.Entity<Notebook>().Property(notebook => notebook.Title).IsRequired();
        modelBuilder.Entity<Notebook>().Property(notebook => notebook.Title).HasMaxLength(50);
        modelBuilder.Entity<Notebook>()
            .HasOne(notebook => notebook.User)
            .WithMany(user => user.Notebooks)
            .HasForeignKey(notebook => notebook.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TodoTask>().ToTable("tasks");
        modelBuilder.Entity<TodoTask>().Property(task => task.Title).IsRequired();
        modelBuilder.Entity<TodoTask>().Property(task => task.Title).HasMaxLength(50);
        modelBuilder.Entity<TodoTask>().Property(task => task.Description).HasMaxLength(200);
        modelBuilder.Entity<TodoTask>()
            .HasOne(task => task.Notebook)
            .WithMany(notebook => notebook.TodoTasks)
            .HasForeignKey(task => task.NotebookId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<TodoTask>()
            .HasOne(task => task.User)
            .WithMany(user => user.TodoTasks)
            .HasForeignKey(task => task.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}