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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext _context;
        public DepartmentRepository(CompanyDbContext context):base(context)
        {
            _context = context;
        }

        public async Task< List<Department>> GetByNameAsync(string name)
        {
            return await _context.Departments.Where(D=>D.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
