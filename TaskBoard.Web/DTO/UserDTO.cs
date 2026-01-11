using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Web.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string RowVersion { get; set; } = string.Empty;
    }
}
