using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
