using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.DAL.Models;

namespace Company.G01.BLL.Inerfaces
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        Task<List<Department>> GetByNameAsync(string name);
    }
}
