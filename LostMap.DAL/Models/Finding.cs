using System.ComponentModel.DataAnnotations;

namespace LostMap.DAL.Models
{
    public class Finding : IdBase
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public User Creator { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Found { get; set; }
    }
}
