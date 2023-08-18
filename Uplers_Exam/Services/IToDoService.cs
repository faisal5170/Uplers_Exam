using Uplers_Exam.ViewModels;

namespace Uplers_Exam.Services
{
    public interface IToDoService
    {
        Task CreatePriorityTask(ToDoVM model);
        Task CreateTask(ToDoVM model);
        Task UpdateTask(ToDoVM model);
        Task DeleteTask(int taskId);
        Task<ToDoVM?> GetTaskById(int taskId);
        Task MarkTaskAsDone(int taskId);
        Task<IEnumerable<ToDoVM>> GetAllTasks();
    }
}
