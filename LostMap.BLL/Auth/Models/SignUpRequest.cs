using System.ComponentModel.DataAnnotations;

namespace LostMap.BLL.Auth.Models
{
    public class SignUpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? Phone { get; set; }
    }
}
