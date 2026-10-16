using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public string  Code { get; set; }
        public DateTime CreateAt { get; set; }
        public List<Employee> Employees { get; set; }
        public string? ImageName { get; set; }

    }
}
