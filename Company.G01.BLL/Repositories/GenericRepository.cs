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
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return(IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int? id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E=>E.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
             }
             return await _context.Set<T>().FirstOrDefaultAsync(E => E.Id == id);
        }

        public async Task AddAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
        }
        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
        }
        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
    }
}
