using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ToDoApp.Controllers
{
    public class TasksController : LogBaseController
    {
        private static readonly List<TaskItem> _tasks = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_tasks); 
        }

        [HttpPost]
        public IActionResult Add(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", _tasks);
            }

            _tasks.Add(task);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult IndexJson()
        {
            return Json(_tasks);
        }

        [HttpGet]
        public IActionResult IndexHtml()
        {
            return View(_tasks); 
        }

        [HttpGet]
        public IActionResult _TaskList()
        {
            return PartialView(); 
        }        

        [HttpGet]
        public IActionResult DownloadTxt()
        {
            var sb = new StringBuilder();
            foreach (var task in _tasks)
            {
                sb.AppendLine($"Description: {task.Description}");
                sb.AppendLine($"Start At: {task.StartAt}");
                sb.AppendLine($"End At: {task.EndAt}");
                sb.AppendLine($"Is Active: {task.IsActive}");
                sb.AppendLine();
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/plain", "tasks.txt");
        }

        [HttpGet]
        public IActionResult DownloadJson()
        {
            var json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            var bytes = Encoding.UTF8.GetBytes(json);
            return File(bytes, "application/json", "tasks.json");
        }

        public record TaskItem(
            string Description,
            DateTime StartAt,
            DateTime EndAt,
            bool IsActive
        );
    }
}