using Xpand.PlanetsAPI.Data.Models.Enums;

namespace Xpand.PlanetsAPI.Data.Models
{
    public class Planet
    {
        public int Id { get; set; }

        public int ImageId { get; set; }  

        public Image Image { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int? DescriptionId { get; set; }

        public Description? Description { get; set; }

        public int? CrewId { get; set; }

        public Crew? Crew { get; set; }

        public PlanetStatus Status { get; set; }
    }
}
