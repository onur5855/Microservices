using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Service.Dtos
{
    public class SignUpDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string City { get; set; }
    }
}
