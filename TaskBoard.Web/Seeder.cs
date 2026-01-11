using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;

namespace TaskBoard.Web
{
    public static class Seeder
    {
        public static void SeedDataForDemo(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskBoardDbContext>();

                // Seed data
                if (!context.Users.Any())
                {
                    context.Users.Add(new User
                    {
                        Id = Guid.Parse("5392e9aa-6fe2-480f-9f47-29acb3156b85"),
                        Name = "Kevin Shum",
                        Email = "kevin@shumii.co.uk",
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) // sql server would do this, we do not write to these fields
                    });

                    context.Users.Add(new User
                    {
                        Id = Guid.Parse("5608b6d1-c6b5-4699-a23f-5224cca19b09"),
                        Name = "Bruce Wayne",
                        Email = "bat@man.co.uk",
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) 
                    });

                    context.Users.Add(new User
                    {
                        Id = Guid.Parse("7678288a-acd6-43d8-b7ec-9eef42ab2f0d"),
                        Name = "Clark Kent",
                        Email = "super@man.co.uk",
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) 
                    });
                    context.SaveChanges();
                }

                if (!context.Boards.Any())
                {
                    context.Boards.Add(new Board
                    {
                        Id = Guid.Parse("02185f48-cb66-422a-9d1d-a718570a6290"),
                        Title = "Project Board",
                        Description = "Board for project tasks",
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) 
                    });
                    context.SaveChanges();
                }

                if (!context.BoardTasks.Any())
                {
                    context.BoardTasks.Add(new BoardTask
                    {
                        Id = Guid.Parse("91250301-d791-4241-975f-c49249fe4590"),
                        BoardId = context.Boards.First().Id,
                        Name = "Sample Task",
                        Details = "This is a sample task",
                        Priority = TaskPriorities.Medium,
                        Status = TaskStatuses.InProgress,
                        AssignedUserId = Guid.Parse("5392e9aa-6fe2-480f-9f47-29acb3156b85"),
                        CreatedUserId = Guid.Parse("5392e9aa-6fe2-480f-9f47-29acb3156b85"),
                        CreatedDate = DateTime.UtcNow,
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) 
                    });

                    context.BoardTasks.Add(new BoardTask
                    {
                        Id = Guid.Parse("5d6f314e-45ba-4bc4-a439-d81a8893b4d6"),
                        BoardId = context.Boards.First().Id,
                        Name = "High Priority Task ",
                        Details = "This is a high priority task",
                        Priority = TaskPriorities.High,
                        Status = TaskStatuses.Todo,
                        AssignedUserId = Guid.Parse("5392e9aa-6fe2-480f-9f47-29acb3156b85"),
                        CreatedUserId = Guid.Parse("5392e9aa-6fe2-480f-9f47-29acb3156b85"),
                        CreatedDate = DateTime.UtcNow,
                        RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks) 
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
