using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Inerfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Data.Context;
using Company.G01.DAL.Models;

namespace Company.G01.BLL
{
    public class UnitOfWork : IUniteOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository {  get; }
        private readonly CompanyDbContext _Context ;


        public UnitOfWork(CompanyDbContext context)
        {
            _Context = context;
            DepartmentRepository=new DepartmentRepository(_Context);
            EmployeeRepository=new EmployeeRepository(_Context);
        }

        public async Task<int> CompleteAsync()
        {
          return await _Context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _Context.DisposeAsync();  
        }
    }
}
