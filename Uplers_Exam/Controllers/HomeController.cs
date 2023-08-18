using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Uplers_Exam.Repository;
using Uplers_Exam.Services;
using Uplers_Exam.ViewModels;

namespace Uplers_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoService _todoService;

        public HomeController(ILogger<HomeController> logger, IToDoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _todoService.GetAllTasks();
            return View(tasks);
        }

        public async Task<IActionResult> CreatePriorityTask(ToDoVM task)
        {
            await _todoService.CreatePriorityTask(task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(ToDoVM task)
        {
            await _todoService.CreateTask(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _todoService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(int id, ToDoVM task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }
            await _todoService.UpdateTask(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            await _todoService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAsDone(int id)
        {
            await _todoService.MarkTaskAsDone(id);
            return RedirectToAction("Index");
        }
    }
}