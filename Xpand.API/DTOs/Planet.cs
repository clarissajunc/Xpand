using Xpand.API.Domain.Models.Enums;
using Xpand.API.Domain.Models;

namespace Xpand.API.DTOs
{
    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[] Image { get; set; } = null!;

        public PlanetStatus Status { get; set; }

        public string? Description { get; set; }

        public Human? DescriptionAuthor { get; set; }

        public int? CrewId { get; set; }

        public ICollection<string> Robots { get; set; } = new List<string>();
    }
}
