using Company.G01.DAL.Models;

namespace Company.G01.PL.Dtos
{
    public class EditDepartmentDtos:BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }


        public DateTime CreateAt { get; set; }


    }
}
