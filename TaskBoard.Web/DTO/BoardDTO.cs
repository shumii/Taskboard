using System.ComponentModel.DataAnnotations;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;

namespace TaskBoard.Web.DTO
{
    public class BoardDTO 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public Dictionary<TaskStatuses, List<BoardTaskSummaryDTO>> TasksByStatus { get; set; } = new Dictionary<TaskStatuses, List<BoardTaskSummaryDTO>>();
        public string RowVersion { get; set; } = string.Empty;
    }
}
