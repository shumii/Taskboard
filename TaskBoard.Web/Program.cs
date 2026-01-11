using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Application.Services;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Web;
using TaskBoard.Web.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

DependencyRegistrations.RegisterDependencies(builder.Services);

builder.Services.AddDbContext<TaskBoardDbContext>(options =>
    options.UseInMemoryDatabase("TaskBoardDb"));

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TaskBoard API v1");
    });
}


app.MapGet("/taskboard", async (Guid taskBoardId, ITaskBoardService taskBoardService) =>
{
    var board = await taskBoardService.GetBoardAsync(taskBoardId);

    if (board != null)
    {
        var tasksByStatus = board.Tasks.GroupBy(g => g.Status)
            .ToDictionary(g => g.Key, g => g.Select(t => new BoardTaskSummaryDTO
            {
                Id = t.Id,
                BoardId = t.BoardId,
                Name = t.Name,                
                Priority = t.Priority,
                Status = t.Status,                
                RowVersion = Convert.ToBase64String(t.RowVersion ?? Array.Empty<byte>())
            }).ToList());

        var boardDTO = new BoardDTO
        {
            Id = board.Id,
            Title = board.Title,
            Description = board.Description,
            TasksByStatus = tasksByStatus,
            RowVersion = Convert.ToBase64String(board.RowVersion ?? Array.Empty<byte>())
        };
        return boardDTO;
    }

    return null; // returning null for now but can handle in many ways (404, custom error object, etc.)
})
.WithDescription("Retrieve the whole object graph for a board and its tasks");

app.MapGet("/task", async (Guid taskId, ITaskBoardService taskBoardService) =>
{
    var task = await taskBoardService.GetBoardTaskAsync(taskId);

    if (task != null)
    {
        var taskDTO = new BoardTaskDTO
        {
            Id = task.Id,
            BoardId = task.BoardId,
            Name = task.Name,
            Priority = task.Priority,
            Status = task.Status,
            AssignedUser = task.AssignedUser,
            AssignedUserId = task.AssignedUserId,
            CreatedUser = task.CreatedUser,
            CreatedUserId = task.CreatedUserId,
            CreatedDate = task.CreatedDate,
            Details = task.Details,
            RowVersion = Convert.ToBase64String(task.RowVersion ?? Array.Empty<byte>())
        };
        return taskDTO;
    }

    return null;
})
.WithDescription("Retrieve the whole object graph for a board and its tasks");

app.MapPut("/task", async (BoardTaskDTO boardTaskDTO, ITaskBoardService taskBoardService) =>
{
    var boardTask = new BoardTask
    {
        Id = boardTaskDTO.Id ?? Guid.Empty,
        BoardId = boardTaskDTO.BoardId,
        Name = boardTaskDTO.Name,
        Details = boardTaskDTO.Details,
        Priority = boardTaskDTO.Priority,
        Status = boardTaskDTO.Status,
        AssignedUserId = boardTaskDTO.AssignedUserId,
        CreatedUserId = boardTaskDTO.CreatedUserId,
        CreatedDate = boardTaskDTO.CreatedDate,
        RowVersion = Convert.FromBase64String(boardTaskDTO.RowVersion)
    };

    try
    {
        await taskBoardService.UpsertTaskAsync(boardTask);
        return Results.Ok();
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict("The task was updated by another user. Please refresh and try again.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while adding the task: {ex.Message}");
    }
})
.WithDescription("Create or update a task");

app.MapDelete("/task", async (Guid taskId, ITaskBoardService taskBoardService) =>
{
    try
    {
        await taskBoardService.DeleteTaskAsync(taskId);
        return Results.Ok();
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict("The task was updated by another user. Please refresh and try again.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while adding the task: {ex.Message}");
    }
})
.WithDescription("Delete a task");

app.MapPost("/task/status", async (UpdateTaskStatusRequest request, ITaskBoardService taskBoardService) =>
{
    try
    {
        await taskBoardService.UpdateTaskStatusAsync(request.TaskId, request.Status);
        return Results.Ok();
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict("The task was updated by another user. Please refresh and try again.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while adding the task: {ex.Message}");
    }
})
.WithDescription("Update a task status only");

Seeder.SeedDataForDemo(app);

app.Run();