using Company.G01.BLL;
using Company.G01.BLL.Inerfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Data.Context;
using Company.G01.DAL.Models;
using Company.G01.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Company.G01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUniteOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.AddDbContext<CompanyDbContext>(Options=>
            {
                Options.UseSqlServer("Server=.;Database=CompanyMvcG01;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            });
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
