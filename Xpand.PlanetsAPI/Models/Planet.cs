using Xpand.PlanetsAPI.Models.Enums;

namespace Xpand.PlanetsAPI.Models
{
    public class Planet
    {
        public int Id { get; set; }

        public int ImageId { get; set; }  

        public Image Image { get; set; } = null!;

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? DescriptionAuthorId { get; set; }

        public Captain? DescriptionAuthor { get; set; }

        public PlanetStatus Status { get; set; }
    }
}
