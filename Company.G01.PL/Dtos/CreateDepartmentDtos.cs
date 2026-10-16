using System.ComponentModel.DataAnnotations;
using Company.G01.DAL.Models;

namespace Company.G01.PL.Dtos
{
    public class CreateDepartmentDtos:BaseEntity
    {
        [Required(ErrorMessage ="Name is Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Required !")]
        public string Code { get; set; }

        
        [Required(ErrorMessage = "CreateAt is Required !")]
        public DateTime CreateAt { get; set; }
        public IFormFile? image { get; set; }
        public string? ImageName { get; set; }

    }
}
