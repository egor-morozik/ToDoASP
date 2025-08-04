using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ToDoASP.Areas.Task.Controllers
{
    public class TasksController : LogBaseController
    {
        private static readonly List<TaskItem> _tasks = new();

        public IActionResult Index()
        {
            return View(_tasks);
        }

        public IActionResult Add(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", _tasks);
            }

            _tasks.Add(task);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult IndexJson()
        {
            return Json(_tasks);
        }

        public IActionResult IndexHtml()
        {
            return View(_tasks);
        }

        public IActionResult _TaskList()
        {
            return PartialView();
        }

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