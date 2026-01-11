using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Domain.Model;

public class User : BaseModel
{    
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Timestamp]
    public byte[] RowVersion { get; set; }
}

