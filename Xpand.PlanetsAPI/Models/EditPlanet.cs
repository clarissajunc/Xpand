using Xpand.PlanetsAPI.Data.Models.Enums;

namespace Xpand.PlanetsAPI.Models
{
    public class EditPlanet
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public int? DescriptionAuthorId { get; set; }

        public PlanetStatus PlanetStatus { get; set; }
    }
}
