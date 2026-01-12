using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Data.Repository;

public class TaskRepository : BaseRepository<BoardTask>, ITaskRepository
{
    public TaskRepository(TaskBoardDbContext context) : base(context)
    {        
    }
    public async Task UpdateStatus(Guid taskId, int status)
    {
        var task = await context.BoardTasks.FindAsync(taskId);
        if (task != null)
        {
            task.Status = (TaskStatuses)status;
            context.BoardTasks.Update(task);
        }
        else
        {
            throw new KeyNotFoundException($"BoardTask with Id {taskId} not found.");
        }
    }
    public async Task<List<BoardTask>> GetTasksByBoardIdAsync(Guid boardId)
    {
        return await context.BoardTasks
            .Where(t => t.BoardId == boardId)            
            .ToListAsync();
    }     
}