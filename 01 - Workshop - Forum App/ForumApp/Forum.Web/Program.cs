using Forum.Core.Contracts;
using Forum.Core.Services;
using Forum.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Default");
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ForumDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPostService,PostService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}