using System.ComponentModel.DataAnnotations;

namespace SimpleSts.Web.Controllers
{
    public class SignupInputModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        [RegularExpression("[a-zA-Z0-9~!@#$%^&*+=]^", ErrorMessage = "Password must contain atleast one Uppercase, one Lowercase, one Number and One special character in ~!@#$%^&*+= only")]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string RetypePassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string ReturnUrl { get; set; }
    }
}
