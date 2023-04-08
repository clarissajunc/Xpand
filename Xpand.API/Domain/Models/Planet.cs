using Xpand.API.Domain.Models;
using Xpand.API.Domain.Models.Enums;

namespace Xpand.API.Domain.Models
{
    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Image Image { get; set; } = null!;

        public PlanetStatus Status { get; set; }

        public int? DescriptionId { get; set; }

        public Description? Description { get; set; }
        
        public int? CrewId { get; set; }
    }
}
