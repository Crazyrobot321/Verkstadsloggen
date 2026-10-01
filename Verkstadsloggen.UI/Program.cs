using Microsoft.EntityFrameworkCore;
using Verkstadsloggen.Infrastructure;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Interface;
using Verkstadsloggen.Application;

namespace Verkstadsloggen.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<ITimeLogRepository, TimeLogRepository>();

            builder.Services.AddScoped<TimeLogService>();


            builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
