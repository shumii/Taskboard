using Microsoft.EntityFrameworkCore;
using TaskBoard.Application.Services;
using TaskBoard.Domain.DTO;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Extensions;
using TaskBoard.Web.DTO;

namespace TaskBoard.Web
{
    // normally would use controllers
    public static class Endpoints
    {
        public static void AddEndpoints(WebApplication app)
        {
            app.MapGet("/taskboard", async (Guid taskBoardId, ITaskBoardService taskBoardService) =>
            {
                try
                {
                    var board = await taskBoardService.GetBoardAsync(taskBoardId);

                    if (board != null)
                    {
                        var boardDTO = board.ToBoardDTO();
                        return Results.Ok(boardDTO);
                    }

                    return Results.NotFound(); // returning null for now but can handle in many ways (404, custom error object, etc.)
                }
                catch (Exception ex)
                {
                    return Results.Problem($"An error occurred while retrieving the board: {ex.Message}");
                }
            })
            .WithDescription("Retrieve the whole object graph for a board and its tasks");

            app.MapGet("/task", async (Guid taskId, ITaskBoardService taskBoardService) =>
            {
                try
                {
                    var task = await taskBoardService.GetBoardTaskAsync(taskId);

                    if (task != null)
                    {
                        var taskDTO = task.ToBoardTaskDTO();
                        return Results.Ok(taskDTO);
                    }                    

                    return Results.NotFound();
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while retrieving the task.");
                }                
            })
            .WithDescription("Retrieve a task");

            app.MapPost("/task", async (BoardTaskDTO boardTaskDTO, ITaskBoardService taskBoardService) =>
            {
                try
                {
                    var boardTask = new BoardTask
                    {
                        Id = Guid.Empty,
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

                    await taskBoardService.AddTaskAsync(boardTask);
                    return Results.Ok();
                }  
                catch (Exception ex)
                {
                    return Results.Problem($"An error occurred while adding the task: {ex.Message}");
                }
            })
            .WithDescription("Create a task");

            app.MapPut("/task", async (BoardTaskDTO boardTaskDTO, ITaskBoardService taskBoardService) =>
            {
                try
                {
                    if (!boardTaskDTO.Id.HasValue)
                    {
                        throw new InvalidOperationException("Id is null");
                    }

                    var boardTask = new BoardTask
                    {
                        Id = boardTaskDTO.Id.Value,
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

                    await taskBoardService.UpdateTaskAsync(boardTask);
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
            .WithDescription("Update a task");

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
        }
    }
}
