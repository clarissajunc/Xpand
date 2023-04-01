using Microsoft.Extensions.Hosting;

namespace Xpand.PlanetsAPI.Models
{
    public class Captain
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Planet> Planets { get; set; } = new List<Planet>();
    }
}
