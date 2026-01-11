using TaskBoard.Domain.Model;


namespace TaskBoard.Domain.Repository;

public interface ITaskBoardRepository : IRepository<Board>
{
    Task<Board?> GetTaskBoardByIdAsync(Guid id);
}