using Uplers_Exam.Command.ToDo;
using Uplers_Exam.Models;
using Uplers_Exam.Repository;

namespace Uplers_Exam.Command
{
    public class CreateToDoCommandHandler : ICommandHandler<CreateToDoCommand>
    {
        private readonly IToDoRepository _taskRepository;

        public CreateToDoCommandHandler(IToDoRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(CreateToDoCommand command)
        {
           await _taskRepository.CreateTask(command._task);
        }
    }
}
