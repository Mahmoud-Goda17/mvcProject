using AutoMapper;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;

namespace Company.G01.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDtos, Employee>();
            CreateMap<Employee, CreateEmployeeDtos>();
                //.ForMember(d=>d.DepartmentName,o=>o.MapFrom(S=>S.Department.Name));

        }
    }
}
