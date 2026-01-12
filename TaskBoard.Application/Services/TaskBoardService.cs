using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Application.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly IUnitOfWork unitOfWork;

        public TaskBoardService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Board?> GetBoardAsync(Guid id)
        {            
            return await unitOfWork.Boards.GetTaskBoardByIdAsync(id);
        }

        public async Task<BoardTask?> GetBoardTaskAsync(Guid taskId)
        {
            return await unitOfWork.Tasks.GetByIdAsync(taskId);
        }

        public async Task DeleteTaskAsync(Guid taskId)
        {
            await unitOfWork.Tasks.DeleteAsync(taskId);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddTaskAsync(BoardTask task)
        {
            await unitOfWork.Tasks.AddAsync(task);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(BoardTask task)
        {
            await unitOfWork.Tasks.UpdateAsync(task);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(Guid taskId, int status)
        {
            await unitOfWork.Tasks.UpdateStatus(taskId, status);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
