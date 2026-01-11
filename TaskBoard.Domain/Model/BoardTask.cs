using System.ComponentModel.DataAnnotations;
using TaskBoard.Domain.Model.Enums;

namespace TaskBoard.Domain.Model;

public class BoardTask : BaseModel
{    
    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;   
    public TaskPriorities Priority { get; set; } = TaskPriorities.Low;
    public TaskStatuses Status { get; set; } = TaskStatuses.Todo;
    public Guid? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; } 
    public Guid CreatedUserId { get; set; }
    public User CreatedUser { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Timestamp]
    public byte[] RowVersion { get; set; }

}

