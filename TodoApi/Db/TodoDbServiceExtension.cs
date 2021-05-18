using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.Db
{
    public static class TodoDbServiceExtension
    {
        public static void AddInMemoryDatabase(this IServiceCollection services, string name)
        {
            services.AddDbContext<TodoDbContext>(options =>
               options.UseInMemoryDatabase(name)
            );
        }

        public static void InitializeSeededData(this IApplicationBuilder app)
        {
            using var service = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            TodoDbSeeder.Seed(service.ServiceProvider);
        }
    }
}
