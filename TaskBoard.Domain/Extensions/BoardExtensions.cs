using System;
using System.Collections.Generic;
using System.Text;
using TaskBoard.Domain.DTO;
using TaskBoard.Domain.Model;

namespace TaskBoard.Domain.Extensions
{
    public static class BoardExtensions
    {
        public static BoardDTO ToBoardDTO(this Board board)
        {
            var tasksByStatus = board.Tasks.GroupBy(g => g.Status)
                            .ToDictionary(g => g.Key, g => g.Select(t => t.ToBoardTaskSummaryDTO()).ToList());

            var boardDTO = new BoardDTO
            {
                Id = board.Id,
                Title = board.Title,
                Description = board.Description,
                TasksByStatus = tasksByStatus,
                RowVersion = Convert.ToBase64String(board.RowVersion ?? Array.Empty<byte>())
            };

            return boardDTO;
        }
    }
}
