using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Verkstadsloggen.Application;
using Verkstadsloggen.Application.Interface;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Repository;

namespace Verkstadsloggen.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            //Register the repositories with dependency injection
            builder.Services.AddScoped<IMechanicRepository, MechanicRepository>();
            builder.Services.AddScoped<IJobRepository, JobRepository>();

            builder.Services.AddScoped<ITimeLogRepository, TimeLogRepository>();
            builder.Services.AddScoped<ITimeLogService, TimeLogService>();

            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IPersonService, PersonService>();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                    options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
