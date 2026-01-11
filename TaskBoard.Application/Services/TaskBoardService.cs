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

        public async Task UpsertTaskAsync(BoardTask task)
        {
            await unitOfWork.Tasks.UpsertAsync(task);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(Guid taskId, int status)
        {
            var task = await unitOfWork.Tasks.GetByIdAsync(taskId);

            if (task != null)
            {
                task.Status = (TaskStatuses)status;
                await unitOfWork.Tasks.UpsertAsync(task);
                await unitOfWork.SaveChangesAsync();                
            }
            else
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }
        }
    }
}
