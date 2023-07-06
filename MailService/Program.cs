
using MailService.Data;
using MailService.Services;
using Microsoft.EntityFrameworkCore;

namespace MailService
{
    public class Program
    {
        /// <summary>
        /// Инициализация ASP.NET core minimal API
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<MailContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IMailingService, MailingService>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var r = app.Configuration;
            using (var scope = app.Services.CreateScope())
            {
                using (var t = scope.ServiceProvider.GetRequiredService<MailContext>())
                {
                    if (!Directory.Exists("./DB"))
                        Directory.CreateDirectory("./DB");
                    t.Database.Migrate();
                }
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}