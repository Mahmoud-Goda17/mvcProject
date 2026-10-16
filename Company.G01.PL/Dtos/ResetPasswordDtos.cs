using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class ResetPasswordDtos
    {
        [Required(ErrorMessage = "Password IS REquired ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword IS REquired ")]
        [Compare("NewPassword", ErrorMessage = "ConfirmPassword Dose not match Newpassword")]
        public string ConfirmPassword { get; set; }

    }
}
