namespace TaskBoard.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskBoardRepository Boards { get; }
        ITaskRepository Tasks { get; }
        Task<int> SaveChangesAsync();
    }
}
