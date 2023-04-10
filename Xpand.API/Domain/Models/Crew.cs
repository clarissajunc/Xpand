namespace Xpand.API.Domain.Models
{
    public class Crew
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int CaptainId { get; set; }

        public Human Captain { get; set; } = null!;

        public ICollection<Robot> Robots { get; set; } = new List<Robot>();
    }
}
