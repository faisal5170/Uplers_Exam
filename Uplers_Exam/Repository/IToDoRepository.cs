using Uplers_Exam.Models;

namespace Uplers_Exam.Repository
{
    public interface IToDoRepository
    {
        Task<ToDoModel?> GetTaskById(int id);
        Task<IEnumerable<ToDoModel>> GetAllTasks();
        Task CreateTask(ToDoModel task);
        Task UpdateTask(ToDoModel task);
        Task DeleteTask(int id);
    }
}
