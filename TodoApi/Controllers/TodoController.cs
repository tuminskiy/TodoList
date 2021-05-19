using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TodoApi.Db;
using TodoApi.Db.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoDbService _todoDbService;
        private IHubContext<TodoApiHub> _hubContext;

        public TodoController(TodoDbService todoDbService, IHubContext<TodoApiHub> hubContext)
        {
            _todoDbService = todoDbService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_todoDbService.Todos());
        }

        [HttpPut]
        public IActionResult UpdateTodo([FromBody] Todo todo)
        {
            if (_todoDbService.UpdateTodo(todo))
            {
                _hubContext.Clients.All.SendAsync("NotifyTodosChange", todo);
            }            

            return Ok();
        }

        [HttpPost]
        public IActionResult AddTodo([FromBody] Todo todo)
        {
            _todoDbService.AddTodo(todo);
            
            _hubContext.Clients.All.SendAsync("NotifyTodosChange", todo);

            return Ok();
        }

    }
}
