using LostMap.DAL.Models;

namespace LostMap.BLL.Models
{
    public class FindingDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Found { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public static FindingDto? Create(Finding finding) =>
            finding == null 
            ? null 
            : new()
            {
                Id = finding.Id,
                Title = finding.Title,
                Description = finding.Description,
                Found = finding.Found,
                Latitude = finding.Latitude,
                Longitude = finding.Longitude
            };
    }
}
