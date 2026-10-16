using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.DAL.Models;

namespace Company.G01.BLL.Inerfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int? id);
       
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);

    }
}
