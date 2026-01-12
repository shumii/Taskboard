using System.ComponentModel.DataAnnotations;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Model.Enums;

namespace TaskBoard.Domain.DTO
{
    public class BoardTaskDTO
    {
        public Guid? Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public TaskPriorities Priority { get; set; } 
        public TaskStatuses Status { get; set; } 
        public Guid? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
        public Guid CreatedUserId { get; set; }
        public User CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string RowVersion { get; set; }
    }
}
