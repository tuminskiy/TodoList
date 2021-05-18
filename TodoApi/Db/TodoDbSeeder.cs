using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodoApi.Db.Models;

namespace TodoApi.Db
{
    public class TodoDbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new TodoDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>()
            );

            if (context.Todos.Any())
            {
                return;
            }

            context.Todos.AddRange(
                new Todo
                {
                    Id = 1,
                    Priority = TodoPriority.High,
                    Case = "Write first todo"
                },
                new Todo
                {
                    Id = 2,
                    Priority = TodoPriority.Medium,
                    Case = "Write second todo"
                },
                new Todo
                {
                    Id = 3,
                    Priority = TodoPriority.Low,
                    Case = "Write third todo"
                },
                new Todo
                {
                    Id = 4,
                    Case = "Write fourth todo"
                }
            );

            context.SaveChanges();
        }
    }
}
