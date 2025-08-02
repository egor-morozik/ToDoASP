using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class TasksController : Controller
    {
        private static readonly List<TaskItem> _tasks = new();

        [HttpGet]
        public IActionResult Index()
        {
            string content = @"
                <h2>Add New Task</h2>
                <form method='post' action='/Tasks/Add'>
                    <label>Description:</label><br/>
                    <input name='Description' required /><br/>
                    <label>Start At:</label><br/>
                    <input type='datetime-local' name='StartAt' required /><br/>
                    <label>End At:</label><br/>
                    <input type='datetime-local' name='EndAt' required /><br/>
                    <label>Is Active:</label><br/>
                    <input type='checkbox' name='IsActive' value='true' checked /><br/>
                    <button type='submit'>Add Task</button>
                </form>
                <h2>Tasks List</h2>
                <a href='/Tasks/IndexHtml'>View as HTML</a>  
                <a href='/Tasks/IndexJson'>View as JSON</a>";
            return Content(content, "text/html");
        }

        [HttpPost]
        public IActionResult Add(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return Content("<h2>Invalid input</h2>", "text/html");
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
            string content = @"<h2>Tasks List</h2>
                            <ul>";
            foreach (var task in _tasks)
            {
                content += $"<li>{task.Description} (Active: {task.IsActive})</li>";
            }
            content += "</ul>";

            return Content(content, "text/html");
        }

        public record TaskItem(
            string Description,
            DateTime StartAt,
            DateTime EndAt,
            bool IsActive
        );
    }
}