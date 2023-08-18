using AutoMapper;
using Moq;
using Uplers_Exam.Command;
using Uplers_Exam.Command.ToDo;
using Uplers_Exam.Factory;
using Uplers_Exam.Models;
using Uplers_Exam.Repository;
using Uplers_Exam.Services;
using Uplers_Exam.ViewModels;

namespace Uplers_Exam.Tests
{
    public class ToDoServiceTests
    {
        private ToDoService _todoService;
        private Mock<IToDoRepository> _mockRepository;
        private Mock<IToDoFactory> _todoFactory;
        private Mock<ICommandHandler<CreateToDoCommand>> _createToDoCommandHandler;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IToDoRepository>();
            _todoFactory = new Mock<IToDoFactory>();
            _createToDoCommandHandler = new Mock<ICommandHandler<CreateToDoCommand>>();
            _mapper = new Mock<IMapper>();
            _todoService = new ToDoService(_mockRepository.Object, _todoFactory.Object, _createToDoCommandHandler.Object, _mapper.Object);
        }

        [Test]
        public async Task CreateTask_ShouldCallRepositoryCreateTask()
        {
            // Arrange
            var taskViewModel = new ToDoVM { Title = "Title", Description = "Description", Priority = PriorityEnum.High, CreatedOn = DateTime.UtcNow, DueDate = DateTime.UtcNow };

            // Act
            await _todoService.CreateTask(taskViewModel);

            // Assert
            _mockRepository.Verify(r => r.CreateTask(It.IsAny<ToDoModel>()), Times.Once);
        }

        [Test]
        public async Task UpdateTask_ShouldUpdateTask()
        {
            // Arrange
            var taskViewModel = new ToDoVM { Id = 1, Title = "Updated Title", Description = "Updated Description", Priority = PriorityEnum.High, CreatedOn = DateTime.UtcNow, DueDate = DateTime.UtcNow };
            var existingTask = new ToDoModel { Id = 1, Title = "Existing Task", Description = "Existing Description", Priority = PriorityEnum.High, };
            _mockRepository.Setup(r => r.GetTaskById(taskViewModel.Id)).ReturnsAsync(existingTask);

            // Act
            await _todoService.UpdateTask(taskViewModel);

            // Assert
            _mockRepository.Verify(r => r.UpdateTask(It.Is<ToDoModel>(m => m.Id == 1 && m.Title == "Updated Title" && m.Description == "Updated Description" && m.Priority == PriorityEnum.High)), Times.Once);
        }

        [Test]
        public async Task DeleteTask_ShouldDeleteTask()
        {
            // Arrange
            var taskIdToDelete = 1;

            // Act
            await _todoService.DeleteTask(taskIdToDelete);

            // Assert
            _mockRepository.Verify(r => r.DeleteTask(taskIdToDelete), Times.Once);
        }

        [Test]
        public async Task GetTaskById_ExistingId_ShouldReturnTask()
        {
            // Arrange
            var taskIdToRetrieve = 1;
            var expectedTask = new ToDoModel { Id = taskIdToRetrieve, Title = "Test Task" };
            _mockRepository.Setup(r => r.GetTaskById(taskIdToRetrieve)).ReturnsAsync(expectedTask);

            // Auto Mapper mapping
            var expectedVM = new ToDoVM { Id = expectedTask.Id, Title = expectedTask.Title }; 
            _mapper.Setup(m => m.Map<ToDoVM>(It.IsAny<ToDoModel>())).Returns(expectedVM);


            // Act
            var result = await _todoService.GetTaskById(taskIdToRetrieve);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(taskIdToRetrieve));
                Assert.That(result.Title, Is.EqualTo(expectedTask.Title));
            });
        }

        [Test]
        public async Task GetTaskById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingTaskId = 999;
            _mockRepository.Setup(r => r.GetTaskById(nonExistingTaskId)).ReturnsAsync((ToDoModel)null);

            // Act
            var result = await _todoService.GetTaskById(nonExistingTaskId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoModel>
            {
                new ToDoModel { Id = 1, Title = "Task 1", Description = "Description 1" },
                new ToDoModel { Id = 2, Title = "Task 2", Description = "Description 2" },
                new ToDoModel { Id = 3, Title = "Task 3", Description = "Description 3"}
            };
            _mockRepository.Setup(r => r.GetAllTasks()).ReturnsAsync(tasks);

            // AutoMapper mapping
            var mappedTasks = tasks.Select(task => new ToDoVM { Id = task.Id, Title = task.Title, Description = task.Description });
            _mapper.Setup(m => m.Map<IEnumerable<ToDoVM>>(It.IsAny<IEnumerable<ToDoModel>>())).Returns(mappedTasks);

            // Act
            var result = await _todoService.GetAllTasks();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
        }
    }
}
