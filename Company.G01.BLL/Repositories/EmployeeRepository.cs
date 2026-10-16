using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Inerfaces;
using Company.G01.DAL.Data.Context;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _Context;
        public EmployeeRepository(CompanyDbContext context):base(context) 
        {
            _Context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _Context.Employees.Include(E=>E.Department).Where(E=>E.Name.ToLower().Contains(name.ToLower())).ToListAsync();  

        }
    }
        
}
