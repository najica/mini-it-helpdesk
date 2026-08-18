
using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace MiniItHelpdesk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Serialize enums as strings instead of numbers
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
          //  if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
