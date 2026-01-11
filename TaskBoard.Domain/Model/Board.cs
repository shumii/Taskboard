using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Domain.Model;

public class Board : BaseModel
{    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;   
    public List<BoardTask> Tasks { get; set; } = new List<BoardTask>();

    [Timestamp]
    public byte[] RowVersion { get; set; }
}

