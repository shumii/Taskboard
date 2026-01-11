using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskBoardDbContext context;
        public ITaskBoardRepository Boards { get; }
        public ITaskRepository Tasks { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(
            TaskBoardDbContext context,
            ITaskBoardRepository boards,
            ITaskRepository tasks,
            IUserRepository users)
        {
            this.context = context;
            Boards = boards;
            Tasks = tasks;
            Users = users;
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }                
        }

        public void Dispose()
        {
            context.Dispose();
        }   
    }
}
