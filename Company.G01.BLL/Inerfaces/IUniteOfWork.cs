using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Inerfaces
{
    public interface IUniteOfWork:IAsyncDisposable
    {
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        Task<int> CompleteAsync();
    }
}
