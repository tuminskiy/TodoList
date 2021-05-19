using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TodoApi.Db;
using TodoApi.Db.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
//using Microsoft.AspNetCore.Mvc

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(opt =>
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
                );
            //.AddNewtonsoftJson(options =>
            //  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );

            services.AddXpoDefaultUnitOfWork(true, options => options
                .UseConnectionString(
                    MSSqlConnectionProvider.GetConnectionString(
                        Configuration["DbServer"], Configuration["DbName"]
                    )
                )
                .UseAutoCreationOption(AutoCreateOption.None)
                .UseEntityTypes(typeof(TodoXp))
            );

            services.AddScoped<TodoDbService>();

            services.AddCors( options =>
                options.AddPolicy(Configuration["CorsPolicy"], builder =>
                {
                    builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                })
            );

            services.AddSignalR();

            services.AddResponseCompression( options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" }
                );
            });

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Configuration["CorsPolicy"]);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TodoApiHub>("/TodoApiHub");
            });
        }
    }
}
