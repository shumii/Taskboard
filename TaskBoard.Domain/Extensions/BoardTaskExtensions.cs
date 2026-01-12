using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskBoard.Domain.DTO;
using TaskBoard.Domain.Model;

namespace TaskBoard.Domain.Extensions
{
    public static class BoardTaskExtensions
    {
        public static BoardTaskSummaryDTO ToBoardTaskSummaryDTO(this BoardTask task)
        {
            var boardTaskDTO = new BoardTaskSummaryDTO
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status,
                RowVersion = Convert.ToBase64String(task.RowVersion ?? Array.Empty<byte>())
            };

            return boardTaskDTO;
        }

        public static BoardTaskDTO ToBoardTaskDTO(this BoardTask task)
        {
            var boardTaskDTO = new BoardTaskDTO
            {
                Id = task.Id,
                BoardId = task.BoardId,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status,
                AssignedUser = task.AssignedUser,
                AssignedUserId = task.AssignedUserId,
                CreatedUser = task.CreatedUser,
                CreatedUserId = task.CreatedUserId,
                CreatedDate = task.CreatedDate,
                Details = task.Details,
                RowVersion = Convert.ToBase64String(task.RowVersion ?? Array.Empty<byte>())
            };

            return boardTaskDTO;
        }
    }
}
