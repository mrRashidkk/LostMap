using System.ComponentModel.DataAnnotations;

namespace LostMap.DAL.Models
{
    public class Status : IdBase
    {
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
