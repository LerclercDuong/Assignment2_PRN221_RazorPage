using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
            builder.Services.AddScoped<INewsArticleService, NewsArticleService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", options => { options.LoginPath = "/Authentication/Login"; });

            // Add Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("3"));
            });
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
