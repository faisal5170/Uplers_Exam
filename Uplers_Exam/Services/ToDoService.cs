using AutoMapper;
using Uplers_Exam.Command;
using Uplers_Exam.Command.ToDo;
using Uplers_Exam.Factory;
using Uplers_Exam.Models;
using Uplers_Exam.Repository;
using Uplers_Exam.ViewModels;

namespace Uplers_Exam.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _todoRepository;
        private readonly IToDoFactory _todoFactory;
        private readonly ICommandHandler<CreateToDoCommand> _createToDoCommandHandler;
        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository todoRepository, IToDoFactory todoFactory,
            ICommandHandler<CreateToDoCommand> createToDoCommandHandler, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _todoFactory = todoFactory;
            _createToDoCommandHandler = createToDoCommandHandler;
            _mapper = mapper;
        }

        public async Task CreateTask(ToDoVM model)
        {
            var taskModel = _mapper.Map<ToDoModel>(model);
            await _todoRepository.CreateTask(taskModel);
        }

        public async Task CreatePriorityTask(ToDoVM model)
        {
            var taskModel = _mapper.Map<ToDoModel>(model);
            var task = _todoFactory.CreateTask(taskModel);
            var createTaskCommand = new CreateToDoCommand(_todoRepository, task);
            await _createToDoCommandHandler.Handle(createTaskCommand);
        }

        public async Task DeleteTask(int taskId)
        {
            await _todoRepository.DeleteTask(taskId);
        }

        public async Task<IEnumerable<ToDoVM>> GetAllTasks()
        {
            return _mapper.Map<IEnumerable<ToDoVM>>(await _todoRepository.GetAllTasks());
        }

        public async Task<ToDoVM?> GetTaskById(int taskId)
        {
            return _mapper.Map<ToDoVM>(await _todoRepository.GetTaskById(taskId));
        }

        public async Task MarkTaskAsDone(int taskId)
        {
            var task = await _todoRepository.GetTaskById(taskId);
            if (task != null)
            {
                task.IsDone = true;
                await _todoRepository.UpdateTask(task);
            }
        }

        public async Task UpdateTask(ToDoVM model)
        {
            var task = await _todoRepository.GetTaskById(model.Id);
            if (task != null)
            {
                task.Title = model.Title;
                task.Description = model.Description;
                task.Priority = model.Priority;
                await _todoRepository.UpdateTask(task);
            }
        }
    }
}
