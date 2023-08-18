using Uplers_Exam.Models;

namespace Uplers_Exam.Factory
{
    public interface IToDoFactory
    {
        ToDoModel CreateTask(ToDoModel model);
    }
}
