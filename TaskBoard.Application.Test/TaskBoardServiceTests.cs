using Moq;
using TaskBoard.Application.Services;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Application.Test
{
    public class TaskBoardServiceTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly TaskBoardService taskBoardService;
        private readonly Mock<ITaskRepository> mockTaskRepository;
        public TaskBoardServiceTests()
        {
            mockTaskRepository = new Mock<ITaskRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            taskBoardService = new TaskBoardService(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(uow => uow.Tasks).Returns(mockTaskRepository.Object);
        }

        [Fact]
        public async Task DeleteTask_CallsUnitOfWorkTaskDelete()
        {
            // Arrange

            var taskId = Guid.NewGuid();

            // Act

            await taskBoardService.DeleteTaskAsync(taskId);

            // Assert

            mockUnitOfWork.Verify(uow => uow.Tasks.DeleteAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_CallsSave()
        {
            // Arrange

            var taskId = Guid.NewGuid();            

            // Act

            await taskBoardService.DeleteTaskAsync(taskId);

            // Assert

            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }        
    }
}
