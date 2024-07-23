using EmployeeTestAPI.Data;
using EmployeeTestAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add builder for EF SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
            builder.Services.AddCors(options =>
            {
                var frontendURL = builder.Configuration.GetValue<string>("frontend_url");

                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
                });
            });

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
