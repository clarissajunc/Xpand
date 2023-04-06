using Xpand.API.Models.Enums;

namespace Xpand.API.Models
{
    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Image Image { get; set; }

        public PlanetStatus Status { get; set; }

        public string Description { get; set; }

        public Human? DescriptionAuthor { get; set; }
        
        public int? CrewId { get; set; }

        public ICollection<string> Robots { get; set; } = new List<string>();
    }
}
