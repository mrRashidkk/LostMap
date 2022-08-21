using System.ComponentModel.DataAnnotations;

namespace LostMap.DAL.Models
{
    public class Role : IdBase
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
