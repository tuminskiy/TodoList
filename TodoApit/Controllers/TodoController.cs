using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
