//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TaskBoard.Application.Services;
//using TaskBoard.Domain.Model;
//using TaskBoard.Web.DTO;

//namespace TaskBoard.Web.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TaskBoardController : ControllerBase
//    {
//        private readonly ITaskBoardService taskBoardService;        

//        public TaskBoardController(ITaskBoardService taskBoardService)
//        {
//            this.taskBoardService = taskBoardService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<BoardDTO>> GetBoard(Guid boardId)
//        {
//            var board = await taskBoardService.GetBoardAsync(boardId);

//            if (board == null)
//                return NotFound();

//            var tasksByStatus = board.Tasks.GroupBy(g => g.Status)
//            .ToDictionary(g => g.Key, g => g.Select(t => new BoardTaskDTO
//            {
//                Id = t.Id,
//                BoardId = t.BoardId,
//                Name = t.Name,
//                Details = t.Details,
//                Priority = t.Priority,
//                Status = t.Status,
//                AssignedUserId = t.AssignedUserId,
//                AssignedUser = t.AssignedUser,
//                CreatedUserId = t.CreatedUserId,
//                CreatedUser = t.CreatedUser,
//                CreatedDate = t.CreatedDate,
//                RowVersion = Convert.ToBase64String(t.RowVersion ?? Array.Empty<byte>())
//            }).ToList());

//            var boardDTO = new BoardDTO
//            {
//                Id = board.Id,
//                Title = board.Title,
//                Description = board.Description,
//                TasksByStatus = tasksByStatus,
//                RowVersion = Convert.ToBase64String(board.RowVersion ?? Array.Empty<byte>())
//            };

//            return Ok(boardDTO);
//        }

//        [HttpPut("task")]
//        public async Task<IActionResult> UpsertTask([FromBody] BoardTaskDTO boardTaskDTO)
//        {         
//            var boardTask = new BoardTask
//            {
//                Id = boardTaskDTO.Id ?? Guid.Empty,
//                BoardId = boardTaskDTO.BoardId,
//                Name = boardTaskDTO.Name,
//                Details = boardTaskDTO.Details,
//                Priority = boardTaskDTO.Priority,
//                Status = boardTaskDTO.Status,
//                AssignedUserId = boardTaskDTO.AssignedUserId,
//                CreatedUserId = boardTaskDTO.CreatedUserId,
//                CreatedDate = boardTaskDTO.CreatedDate,
//                RowVersion = Convert.FromBase64String(boardTaskDTO.RowVersion)
//            };

//            try
//            {
//                await taskBoardService.UpsertTaskAsync(boardTask);
//                return Ok();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Conflict("The task was updated by another user. Please refresh and try again.");
//            }
//            catch (Exception ex)
//            {
//                return Problem($"An error occurred while adding the task: {ex.Message}");
//            }
//        }
//    }
//}
