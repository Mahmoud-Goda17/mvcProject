using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class ForgetPasswordDtos
    {
        [Required(ErrorMessage = "Email IS REquired ")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
