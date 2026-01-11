using TaskBoard.Domain.Repository;
using TaskBoard.Domain.Model;
using TaskBoard.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskBoard.Data.Repository;

public class TaskboardRepository : BaseRepository<Board>, ITaskBoardRepository
{   
    public TaskboardRepository(TaskBoardDbContext context) : base(context)
    {        
    }

    public async Task<Board?> GetTaskBoardByIdAsync(Guid id)
    {
        var boards = await context.Boards
          .Include(b => b.Tasks)
          .ThenInclude(t => t.AssignedUser)
          .Include(b => b.Tasks)
          .ThenInclude(t => t.CreatedUser)
          .SingleOrDefaultAsync(b => b.Id == id);
        return boards;
    }   

}