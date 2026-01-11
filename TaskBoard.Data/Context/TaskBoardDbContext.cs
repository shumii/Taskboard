using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Model;

namespace TaskBoard.Data.Context;

public class TaskBoardDbContext : DbContext
{
    public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardTask> BoardTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Board>().HasKey(b => b.Id);
        modelBuilder.Entity<Board>()
            .Property(b => b.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<BoardTask>().HasKey(t => t.Id);
        modelBuilder.Entity<BoardTask>()
            .Property(t => t.RowVersion)
            .IsRowVersion();
    }
}