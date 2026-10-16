using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.DAL.Data.Context
{
    public class CompanyDbContext : IdentityDbContext<AppUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=CompanyMvcG01;Trusted_Connection=True;TrustServerCertificate=true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; } 
       public DbSet<Employee> Employees { get; set; }


    }
}
