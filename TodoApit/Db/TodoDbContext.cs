using Microsoft.EntityFrameworkCore;
using TodoApit.Models;

namespace TodoApit.Db
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {  }

        public DbSet<Todo> Todos { get; set; }
    }
}
