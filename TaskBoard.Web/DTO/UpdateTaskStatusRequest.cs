namespace TaskBoard.Web.DTO
{
    public class UpdateTaskStatusRequest
    {
        public Guid TaskId { get; set; }
        public int Status { get; set; }
    }
}
