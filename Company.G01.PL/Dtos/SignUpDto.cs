using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName IS REquired ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FristName IS REquired ")]
          public string FristName { get; set; }

        [Required(ErrorMessage = "LastName IS REquired ")]
         public string LastName { get; set; }

        [Required(ErrorMessage = "Email IS REquired ")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password IS REquired ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword IS REquired ")]
        [Compare("Password",ErrorMessage ="ConfirmPassword Dose not match password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }






    }
}
