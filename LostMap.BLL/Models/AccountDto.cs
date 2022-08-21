using LostMap.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace LostMap.BLL.Models
{
    public class AccountDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public static AccountDto? Create(User user) =>
            user == null
            ? null
            : new AccountDto
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };
    }
}
