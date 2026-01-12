using TaskBoard.Domain.Model;

namespace TaskBoard.Domain.Repository;

public interface ITaskRepository : IRepository<BoardTask>
{
    Task UpdateStatus(Guid taskId, int status);
    Task<List<BoardTask>> GetTasksByBoardIdAsync(Guid boardId);
}