using TaskBoard.Domain.Model;

namespace TaskBoard.Domain.Repository;

public interface ITaskRepository : IRepository<BoardTask>
{    
    Task<List<BoardTask>> GetTasksByBoardIdAsync(Guid boardId);
}