using Uplers_Exam.Models;
using Uplers_Exam.Repository;

namespace Uplers_Exam.Command.ToDo
{
    public class CreateToDoCommand : ICommand
    {
        private readonly IToDoRepository _taskRepository;
        public readonly ToDoModel _task;

        public CreateToDoCommand(IToDoRepository taskRepository, ToDoModel task)
        {
            _taskRepository = taskRepository;
            _task = task;
        }

        public void Execute()
        {
            _taskRepository.CreateTask(_task);
        }
    }
}
