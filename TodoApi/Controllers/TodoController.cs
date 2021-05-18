using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TodoApi.Db;
using TodoApi.Db.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        private readonly IHubContext<TodoApiHub> _hubContext;

        public TodoController(TodoDbContext dbContext, IHubContext<TodoApiHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_dbContext.Todos);
        }

        [HttpPut]
        public IActionResult UpdateTodo([FromBody] Todo todo)
        {
            var todoUpdate = _dbContext.Todos.Find(todo.Id);

            if (todoUpdate is null)
            {
                return NotFound();
            }

            todoUpdate.Priority = todo.Priority;
            todoUpdate.Case = todo.Case;

            _dbContext.Todos.Update(todoUpdate);
            
            if (_dbContext.SaveChanges() > 0)
            {
                _hubContext.Clients.All.SendAsync("NotifyTodoAdded", todoUpdate);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddTodo([FromBody] Todo todo)
        {
            var newId = _dbContext.Todos.Select(o => o.Id).Max() + 1;

            todo.Id = newId;

            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
