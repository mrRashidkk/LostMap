using LostMap.DAL.Models;

namespace LostMap.BLL.Models
{
    public class LossDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Lost { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public static LossDto? Create(Loss loss) =>
            loss == null
            ? null
            : new()
            {
                Id = loss.Id,
                Title = loss.Title,
                Description = loss.Description,
                Lost = loss.Lost,
                Latitude = loss.Latitude,
                Longitude = loss.Longitude
            };
    }
}
