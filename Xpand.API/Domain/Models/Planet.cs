using Xpand.API.Domain.Models;
using Xpand.API.Domain.Models.Enums;

namespace Xpand.API.Domain.Models
{
    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Image Image { get; set; }

        public PlanetStatus Status { get; set; }

        public string? Description { get; set; }

        public Human? DescriptionAuthor { get; set; }
        
        public int? CrewId { get; set; }

        public ICollection<string> Robots { get; set; } = new List<string>();
    }
}
