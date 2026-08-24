using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.Middleware;
using MiniItHelpdesk.Services;
using System.Text.Json.Serialization;

namespace MiniItHelpdesk
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            const string FrontendCorsPolicy = "FrontendCorsPolicy";
            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(FrontendCorsPolicy, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            //  if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(FrontendCorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
