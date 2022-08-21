using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMap.DAL.Models
{
    public class User : IdBase
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(32)]
        public string? Phone { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public Role Role { get; set; }

        public Guid RoleId { get; set; }
    }
}
