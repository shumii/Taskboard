using System;
using System.Collections.Generic;
using System.Text;
using TaskBoard.Domain.Model;

namespace TaskBoard.Application.Services
{
    public interface ITaskBoardService
    {
        Task<Board?> GetBoardAsync(Guid id);
        Task<BoardTask?> GetBoardTaskAsync(Guid taskId);
        Task UpsertTaskAsync(BoardTask task);
        Task UpdateTaskStatusAsync(Guid taskId, int status);
        Task DeleteTaskAsync(Guid taskId);        
    }
}
