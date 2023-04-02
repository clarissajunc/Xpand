namespace Xpand.CrewsAPI.Models
{
    public class Robot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CrewId { get; set; }

        public Crew Crew { get; set; } = null!;
    }
}
