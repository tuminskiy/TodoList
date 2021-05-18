using Microsoft.EntityFrameworkCore;
using TodoApi.Db.Models;

namespace TodoApi.Db
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {  }

        public DbSet<Todo> Todos { get; set; }
    }
}
