using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;

namespace TaskBoard.Domain.DTO
{
    public class BoardTaskSummaryDTO
    {
        public Guid? Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; } = string.Empty;        
        public TaskPriorities Priority { get; set; } 
        public TaskStatuses Status { get; set; }
        public string RowVersion { get; set; }
    }
}
