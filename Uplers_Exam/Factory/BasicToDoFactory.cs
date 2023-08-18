using Uplers_Exam.Models;

namespace Uplers_Exam.Factory
{
    public class BasicToDoFactory : IToDoFactory
    {
        public ToDoModel CreateTask(ToDoModel model)
        {
            return new ToDoModel { Title = model.Title, Description = model.Description, DueDate = model.DueDate, Priority = PriorityEnum.Normal };
        }
    }
}
