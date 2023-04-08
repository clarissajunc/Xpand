namespace Xpand.CrewsAPI.Data.Models
{
    public class Human
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Crew? Crew { get; set; }
    }
}
