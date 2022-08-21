using System.ComponentModel.DataAnnotations;

namespace LostMap.DAL.Models
{
    public class Permission : IdBase
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public List<Role> Roles { get; set; }
    }
}
