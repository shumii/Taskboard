using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Data.Repository;

public class TaskRepository : BaseRepository<BoardTask>, ITaskRepository
{
    public TaskRepository(TaskBoardDbContext context) : base(context)
    {        
    }
    public async Task<List<BoardTask>> GetTasksByBoardIdAsync(Guid boardId)
    {
        return await context.BoardTasks
            .Where(t => t.BoardId == boardId)            
            .ToListAsync();
    }   
}