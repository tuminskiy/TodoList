using Microsoft.AspNetCore.Mvc;
using TodoApit.Db;

namespace TodoApit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
