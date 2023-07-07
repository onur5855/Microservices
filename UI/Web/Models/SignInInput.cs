using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name ="Email Adresiniz")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool Remember { get; set; }
    }
}
